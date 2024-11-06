using TheGarageManagerAPP.Models;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP
{
    public partial class App : Application
    {
       
        public UserModels? LoggedInUser { get; set; }

        public List<UserStatusModels> UserStatuses { get; set; } = new List<UserStatusModels>();
        private TheGarageManagerWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, TheGarageManagerWebAPIProxy proxy)
        {
            this.proxy = proxy;
            LoggedInUser = null;
            InitializeComponent();
            LoadBasicDataFromServer();
            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }

        private async void LoadBasicDataFromServer()
        {
            List<UserStatusModels>? statuses = await this.proxy.GetUserStatusAsync();
            if (statuses != null)
            {
                UserStatuses.Clear();
                foreach (UserStatusModels s in statuses)
                {
                    UserStatuses.Add(s);
                }
            }
        }
    }
}

