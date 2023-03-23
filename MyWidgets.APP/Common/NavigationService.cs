using MyWidgets.APP.View;
using System.Windows.Controls;

namespace MyWidgets.APP.Common
{
    public enum PageFlags
    {
        Root,
        Sub,
    }

    public interface IPageFlags
    {
        public PageFlags PageFlag { get; }
    }

    public class NavigationService
    {
        Settings settingView;

        public ItemCollection Items { get; private set; }

        //public ObservableCollection<IPageFlags> Pages => new ObservableCollection<IPageFlags>();



        public void Init(Settings view)
        {

            settingView = view;

            Items = settingView.nav_menu.Items;

            view.nav_menu.SelectionChanged += Nav_menu_SelectionChanged;
        }

        private void Nav_menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var lb = (ListBox)sender;

            var pg = lb.SelectedItem as IPageFlags;

            var index = Items.IndexOf(pg);

            if (ReferenceEquals(pg, settingView.fm.Content))
            {
                return;
            }

            for (int i = Items.Count - 1; i >= 0; i--)
            {
                if (i > index)
                {
                    Items.RemoveAt(i);
                }
            }


            settingView.fm.Navigate(pg);
        }

        public void NavigateTo(IPageFlags pg)
        {
            Items.Clear();

            settingView.fm.Navigate(pg);


            Items.Add(pg);

            settingView.nav_menu.SelectedValue = pg;
        }

        public void NavigateToSub(IPageFlags pg)
        {
            settingView.fm.Navigate(pg);


            Items.Add(pg);


            settingView.nav_menu.SelectedValue = pg;
        }


    }
}
