using MyWidgets.APP.Common;
using MyWidgets.APP.ViewModel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MyWidgets.APP.View
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page, IPageFlags
    {
        public AboutPage()
        {
            InitializeComponent();


            DataContext = new AboutPageVM();
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


        }
        public PageFlags PageFlag => PageFlags.Root;





    }


    public class WorkFlowRun
    {
        public class Workflow_run
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string repository_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string head_repository_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string head_branch { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string head_sha { get; set; }
        }

        public class ArtifactsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string node_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string size_in_bytes { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string archive_download_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string expired { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string created_at { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string updated_at { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string expires_at { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Workflow_run workflow_run { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int total_count { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ArtifactsItem> artifacts { get; set; }
        }
    }
}
