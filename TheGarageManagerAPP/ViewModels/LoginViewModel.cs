using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Models;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private TheGarageManagerWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public LoginViewModel(TheGarageManagerWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            ShowPasswordCommand = new Command(OnShowPassword);
            email = "";
            password = "";
            InServerCall = false;
            errorMsg = "";
        }

        private string email;
        private string password;

        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }


        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }


        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall = true;
            ErrorMsg = "";
            //Call the server to login
            LoginInfoModels loginInfo = new LoginInfoModels { Email = Email, Password = Password };
            UserModels? u = await this.proxy.LoginAsync(loginInfo);

            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                ErrorMsg = "Invalid email or password";
            }
            else
            {
                ErrorMsg = "";
                //Navigate to the main page
                AppShell shell = serviceProvider.GetService<AppShell>();
                TheGarageHomePageViewModel theGarageHomePageViewModel = serviceProvider.GetService<TheGarageHomePageViewModel>();
                //thegaragemanagerViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton
                ((App)Application.Current).MainPage = shell;
                Shell.Current.FlyoutIsPresented = false; //close the flyout
                //Shell.Current.GoToAsync("TheGarageHomaPageView"); //Navigate to the Tasks tab page
            }
        }


        private void OnRegister()
        {
            ErrorMsg = "";
            Email = "";
            Password = "";
            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterView>());
        }


        private bool isPassword = true;
        public bool IsPassword
        {
            get
            {
                return this.isPassword;
            }
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        //This command will trigger on pressing the password eye icon
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
        }

    }
}
