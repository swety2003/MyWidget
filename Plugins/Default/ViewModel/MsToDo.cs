using Azure.Identity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph;
using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace Default.ViewModel
{
    partial class MsToDo : ObservableRecipient
    {

        //[ObservableProperty]
        public GraphServiceClient graphClient;

        public string selectedTaskListId;

        [ObservableProperty]
        private bool isLogin = false;
        [ObservableProperty]
        private ImageSource headImage;

        [ObservableProperty]
        private TodoListsCollectionPage todoLists;

        [ObservableProperty]
        private TodoTaskListTasksCollectionPage todoTasks;

        public MsToDo()
        {
            CreateCilent();

            //Login();
        }

        [RelayCommand]
        private void UpdateTask(object o)
        {
            Task.Run(async () =>
            {

                var todoTask = o as TodoTask;
                await graphClient.Me.Todo.Lists[selectedTaskListId].Tasks[todoTask.Id]
                    .Request()
                    .UpdateAsync(todoTask);

                //GetTasksAsync(selectedTaskListId);
            });
        }


        private void CreateCilent()
        {
            var scopes = new[] { "User.Read" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "common";

            // Value from app registration
            var clientId = "63323d6c-7f31-4fa2-bca8-eec656888e97";

            // using Azure.Identity;
            var options = new InteractiveBrowserCredentialOptions
            {
                TenantId = tenantId,
                ClientId = clientId,
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                // MUST be http://localhost or http://localhost:PORT
                // See https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core
                RedirectUri = new Uri("http://localhost"),
            };

            // https://docs.microsoft.com/dotnet/api/azure.identity.interactivebrowsercredential
            var interactiveCredential = new InteractiveBrowserCredential(options);

            graphClient = new GraphServiceClient(interactiveCredential, scopes);
        }

        [RelayCommand]
        private void Login()
        {
            Task.Run(async () =>
            {
                try
                {
                    await GetListsAsync();

                    var stream = await graphClient.Me.Photo.Content
                        .Request()
                        .GetAsync();
                    ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
                    HeadImage = (ImageSource)imageSourceConverter.ConvertFrom(stream);



                    //graphClient.Me.Todo.Lists[selectedTaskListId].Tasks.
                    //graphClient.Subscriptions.
                }
                catch (Exception ex)
                {
                    //Growl.Error(ex.ToString());
                }

            });
        }

        [RelayCommand]
        private void Refresh()
        {
            Task.Run(async () =>
            {

                await GetListsAsync();
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    //Growl.Info("列表更新成功！");
                });
            });

        }

        private async Task GetListsAsync()
        {
            var lists = await graphClient.Me.Todo.Lists
                .Request()
                .GetAsync();
            TodoLists = (TodoListsCollectionPage)lists;

            if (lists.Count > 0)
            {
                IsLogin = true;
            }
            GetTasksAsync();
        }

        public async Task GetTasksAsync(string? id = null)
        {
            if (id == null)
            {
                id = selectedTaskListId;
            }
            var tasks = await graphClient.Me.Todo.Lists[id].Tasks
                .Request()
                .GetAsync();
            TodoTasks = (TodoTaskListTasksCollectionPage)tasks;
        }
    }
}
