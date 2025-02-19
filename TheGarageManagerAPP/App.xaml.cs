using TheGarageManagerAPP.Models;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP
{
    public partial class App : Application
    {
       
        public UserModels? LoggedInUser { get; set; }

        public List<UserStatusModels> UserStatuses { get; set; } = new List<UserStatusModels>();
        public List<AppointmentStatusModels> AppointmentStatuses { get; set; } = new List<AppointmentStatusModels>();
        private TheGarageManagerWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, TheGarageManagerWebAPIProxy proxy)
        {
            this.proxy = proxy;
            LoggedInUser = null;
            InitializeComponent();
            LoadBasicDataFromServer();
            //Start with the Login View
            //MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
            LoginView? v = serviceProvider.GetService<LoginView>();

            MainPage = new NavigationPage(v);
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

            List<AppointmentStatusModels>? appStatus = await this.proxy.GetAppointmentStatusesAsync();
            if (appStatus != null)
            {
                AppointmentStatuses.Clear();
                foreach (var s in appStatus)
                {
                    AppointmentStatuses.Add(s);
                }
            }
        }
    }
}

