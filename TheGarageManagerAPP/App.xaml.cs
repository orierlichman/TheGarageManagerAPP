using TheGarageManagerAPP.Models;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP
{
    public partial class App : Application
    {
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new AppShell();
        //}
        //public partial class App : Application
        //{
        //    //Application level variables
            public UserModels? LoggedInUser { get; set; }
        //public List<UrgencyLevel> UrgencyLevels { get; set; } = new List<UrgencyLevel>();
        private TheGarageManagerWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, TheGarageManagerWebAPIProxy proxy)
        {
            this.proxy = proxy;
            LoggedInUser = null;
            InitializeComponent();
            //LoadBasicDataFromServer();
            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }

        //    private async void LoadBasicDataFromServer()
        //    {
        //        List<UrgencyLevel>? levels = await this.proxy.GetUrgencyLevels();
        //        if (levels != null)
        //        {
        //            UrgencyLevels.Clear();
        //            foreach (UrgencyLevel level in levels)
        //            {
        //                UrgencyLevels.Add(level);
        //            }
        //        }
        //    }
        //}
    }
}
