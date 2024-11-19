using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerAPP.Models;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        private UserModels currentUser;
        private IServiceProvider serviceProvider;

        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
        }

        public bool IsManager
        {
            get
            {
                if (currentUser.UserStatusID == 1)
                    return false;
                if (currentUser.UserStatusID == 2)
                    return true;
                return false;
            }
        }

        //this command will be used for logout menu item
        public Command LogoutCommand
        {
            get
            {
                return new Command(OnLogout);
            }
        }
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;

            ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }

    }
}
