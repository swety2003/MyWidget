using ChatGPT;
using ChatGPT_GUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace ChatGPT_GUI.ViewModels;

public partial class MainViewModel: ObservableRecipient {


    ILogger<MainViewModel>? logger;

    public MainViewModel(MainView view) {

        this.view = view;
        IsActive = true;
        RingVisibility = Visibility.Collapsed;
        StateMessage = "[等待A (用户)]";

    }


    [RelayCommand]
    async void Loaded() {

        try
        {
            logger = logger ?? PluginSDK.Logger.LoggerFactory.CreateLogger<MainViewModel>();

            OpenAIService = new OpenAIService(new OpenAiOptions() { ApiKey = view.AppConfig.API_Key });
        }
        catch (Exception ex)
        {
            logger?.LogError($"创建OpenAIService失败:{ex.Message}", ex);

        }
        //MessageBox.Show("你好，欢迎使用猫娘模拟器");
        
    }

    [ObservableProperty]
    ObservableCollection<ChatModel> _ChatList = new ObservableCollection<ChatModel>();

    public IOpenAIService? OpenAIService { get; set; }


    [RelayCommand(CanExecute = nameof(IsSendMethod))]
    async void Ask() {
        await action(Message, false);
    }

    [RelayCommand]
    void ShowSetting() {

        view.ShowSetting();
    }

    bool IsSendMethod() {
        return IsSend;
    }


    [RelayCommand(CanExecute = nameof(IsSendMethod))]
    async void SetSystem(string message) {
        await action(message, true);
        Message = "";
    }

    [ObservableProperty]
    Visibility _RingVisibility;

    [ObservableProperty]
    string _message;

    [ObservableProperty]
    string _StateMessage;

    [ObservableProperty]
    private bool isSend = true;
    private readonly MainView view;

    partial void OnIsSendChanged(bool value) {
        AskCommand.NotifyCanExecuteChanged();
        SetSystemCommand.NotifyCanExecuteChanged();
    }


    async Task action(string message,bool issystem) {
        IsSend = false;
        RingVisibility = Visibility.Visible;
        StateMessage = "[等待B (AI)]";
        ChatList.Add(new ChatModel() {
            Type = ChatType.User,
            DateTime = DateTime.Now.AddSeconds(1),
            Message = message
        });
        var messagelist = new List<OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage>();
        
        foreach (var chat in ChatList) {
            switch (chat.Type) {
                case ChatType.User:
                    //用户的回答
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromUser(chat.Message));
                    break;
                case ChatType.AI:
                    //AI的回答
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromAssistance(chat.Message));
                    break;
                case ChatType.System:
                    messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(chat.Message));
                    break;
            }
        }

        if (issystem)
            messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromSystem(message));
        else
            messagelist.Add(OpenAI.GPT3.ObjectModels.RequestModels.ChatMessage.FromUser(message));
        try {
            var result = await OpenAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest() {
                Messages = messagelist,
                Model = OpenAI.GPT3.ObjectModels.Models.ChatGpt3_5Turbo0301
                
            });
            if (result.Successful) {
                string str = "";
                result.Choices.ForEach((val) => {
                    str += string.IsNullOrWhiteSpace(val.Message.Content) ? "" : val.Message.Content;
                });
                ChatList.Add(new ChatModel() {
                    Type = ChatType.AI,
                    DateTime = DateTime.Now.AddSeconds(1),
                    Message = str
                });
            }
            else {
                if (result.Error == null) {
                    throw new Exception("Unknown Error");
                }
                MessageBox.Show($"{result.Error.Message}");
            }
            RingVisibility = Visibility.Collapsed;
            StateMessage = "[等待A (用户)]";
            IsSend = true;
        }
        catch (Exception ex) 
        {
            logger?.LogError(ex.Message, ex);
            RingVisibility = Visibility.Collapsed;
            StateMessage = "[等待A (用户)]";
            IsSend = true;
        }
        
    }
}
