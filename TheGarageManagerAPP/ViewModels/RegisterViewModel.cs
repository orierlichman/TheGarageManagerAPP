﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Models;

namespace TheGarageManagerAPP.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private TheGarageManagerWebAPIProxy proxy;
        public RegisterViewModel(TheGarageManagerWebAPIProxy proxy) 
        {
            this.proxy = proxy;
            RegisterCommand = new Command(OnRegister);
            CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);
            IsPassword = true;
            NameError = "Name is required";
            RashamHavarotError = "RashamHavarot is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            StatusModels = ((App)Application.Current).UserStatuses;
            SelectedStatus = StatusModels[1];

        }


        //Defiine properties for each field in the registration form including error messages and validation logic
        #region RashamHavarot
        private bool showRashamHavarotError;

        public bool ShowRashamHavarotError
        {
            get => showRashamHavarotError;
            set
            {
                showRashamHavarotError = value;
                OnPropertyChanged("ShowRashamHavarotError");
            }
        }

        private string rashamHavarot;

        public string RashamHavarot
        {
            get => rashamHavarot;
            set
            {
                rashamHavarot = value;
                ValidateRashamHavarot();
                OnPropertyChanged("RashamHavarot");
            }
        }

        private string rashamHavarotError;

        public string RashamHavarotError
        {
            get => rashamHavarotError;
            set
            {
                rashamHavarotError = value;
                OnPropertyChanged("RashamHavarotError");
            }
        }

        private void ValidateRashamHavarot()
        {
            this.ShowRashamHavarotError = string.IsNullOrEmpty(RashamHavarot);
        }
        #endregion
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
        private  List<UserStatusModels> statusModels;
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




        //This command will trigger on pressing the password eye icon
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
        }
        #endregion



        //Define a command for the register button
        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnRegister()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidateRashamHavarot();    

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowRashamHavarotError)
            {
                //Create a new AppUser object with the data from the registration form
                var newUser = new UserModels
                {
                    
                    UserFirstName = Name,
                    UserLastName = LastName,
                    Email = Email,
                    UserPassword = Password,
                    UserStatusID = SelectedStatus.StatusID,
                    UserGarageID = 103 //TO DO: Need to replace with a rel garage id
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;

                newUser = await proxy.Register(newUser, RashamHavarot);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    InServerCall = false;
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }
        }

        //Define a method that will be called upon pressing the cancel button
        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }

    }

}

