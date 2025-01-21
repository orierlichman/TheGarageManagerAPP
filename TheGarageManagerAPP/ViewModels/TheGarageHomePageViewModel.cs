using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP.ViewModels
{
    public class TheGarageHomePageViewModel : ViewModelBase
    {

        private TheGarageManagerWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public TheGarageHomePageViewModel(TheGarageManagerWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            PartsCommand = new Command(OnParts);
            AllVehiclesCommand = new Command(OnAllVehicles);
            ProfileCommand = new Command(OnProfile);
            AppointmentCommand = new Command(OnAppointment);
            CarRepairCommand = new Command(OnCarRepair);
        }


        public ICommand PartsCommand { get; }
        public ICommand AllVehiclesCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand AppointmentCommand { get; }
        public ICommand CarRepairCommand { get; }


        private void OnParts()
        {
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<PartsView>());
        }

        private void OnAllVehicles()
        {
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<AllVehiclesView>());
        }
        private void OnProfile()
        {
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<ProfileView>());
        }
        private void OnAppointment()
        {
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<AppointmentView>());
        }
        private void OnCarRepair()
        {
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<CarRepairView>());
        }





    }
}
