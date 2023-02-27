using HandyControl.Controls;
using PluginSDK;
using System;
using System.Windows.Controls;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class MsToDo : UserControl, ICard
    {

        public static CardInfo info = new CardInfo(null, "测试卡片", "描述", typeof(MsToDo));

        public int HeightPix => 5;

        public int WidthPix => 5;

        public Guid GUID { get; private set; }

        ViewModel.MsToDo vm;
        public MsToDo(Guid g)
        {
            InitializeComponent();

            GUID = g;


        }


        public void OnEnabled()
        {

            vm = new ViewModel.MsToDo();

            DataContext = vm;
        }

        public void OnDisabled()
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var id = ((Microsoft.Graph.Entity)((object[])e.AddedItems)[0]).Id.ToString();
                if (id == null)
                {
                    return;
                }
                vm.selectedTaskListId = id;

                vm.GetTasksAsync(vm.selectedTaskListId);
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }
        }
    }
}
