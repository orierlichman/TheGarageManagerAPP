using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerAPP.Models;
using TheGarageManagerApp.Services;

namespace TheGarageManagerAPP.ViewModels
{
    public class ProfileViewModels : ViewModelBase
    {
        private TheGarageManagerWebAPIProxy proxy;
        public ProfileViewModels(TheGarageManagerWebAPIProxy proxy)
        {
            this.proxy = proxy;
            UpdateUserCommand = new Command(OnUpdateUser);
            IsPassword = true;
            NameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            StatusModels = ((App)Application.Current).UserStatuses;
            SelectedStatus = StatusModels[1];
            this.Name = ((App)Application.Current).LoggedInUser.UserFirstName;
            this.lastName = ((App)Application.Current).LoggedInUser.UserLastName;
            this.Email = ((App)Application.Current).LoggedInUser.Email;
            this.Password = ((App)Application.Current).LoggedInUser.UserPassword;
        }


        //Defiine properties for each field in the registration form including error messages and validation logic
        #region Name
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion


        #region LastName
        private bool showLastNameError;

        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;

        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
        }
        #endregion


        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        #endregion


        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 4 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
        }

        //This property will indicate if the password entry is a password
        private bool isPassword = true;
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        private List<UserStatusModels> statusModels;
        public List<UserStatusModels> StatusModels
        {
            get => statusModels;
            set
            {
                statusModels = value;
                OnPropertyChanged("StatusModels");
            }
        }

        private UserStatusModels selectedStatus;
        public UserStatusModels SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                OnPropertyChanged();
            }
        }





        #endregion


        #region Status

        private string status;

        public string Status
        {
            get => status;
            set
            {
                status = value;
                ValidateStatus();
                OnPropertyChanged("Status");
            }
        }

        private bool showStatusError;

        public bool ShowStatusError
        {
            get => showStatusError;
            set
            {
                showStatusError = value;
                OnPropertyChanged("ShowStatusError");
            }
        }

        private string statusError;

        public string StatusError
        {
            get => statusError;
            set
            {
                statusError = value;
                OnPropertyChanged("StatusError");
            }
        }


        private void ValidateStatus()
        {
            this.showStatusError = string.IsNullOrEmpty(Status);
        }
        #endregion



        //Define a command for the register button
        public Command UpdateUserCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnUpdateUser()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError)
            {
                UserModels theUser = ((App)Application.Current).LoggedInUser;
                theUser.UserFirstName = Name;
                theUser.UserLastName = LastName;
                theUser.Email = Email;
                theUser.UserPassword = Password;
                theUser.UserStatusID = SelectedStatus.StatusID;
                theUser.UserGarageID = 103; //TO DO: Need to replace with a rel garage id

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                theUser = await proxy.UpdatUser(theUser);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (theUser != null)
                {
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Save Profile", "Profile saved successfully", "ok");
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "update profile failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("update profile", errorMsg, "ok");
                }
            }
        }


    }
}
