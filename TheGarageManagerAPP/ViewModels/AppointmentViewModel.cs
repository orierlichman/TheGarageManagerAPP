using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Models;

namespace TheGarageManagerAPP.ViewModels
{
    public class AppointmentViewModel : ViewModelBase
    {

        private TheGarageManagerWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public AppointmentViewModel(TheGarageManagerWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            InServerCall = false;
            OnApproveCommand = new Command<AppointmentModels>(OnApprove);
            OnDeclineCommand = new Command<AppointmentModels>(OnDecline);
            InitData();
        }

        private async void InitData()
        {
            List<AppointmentStatusModels> statuses = ((App)Application.Current).AppointmentStatuses;
            AppStatuses = new ObservableCollection<AppointmentStatusModels>(statuses);
            List<AppointmentModels>? list = await this.proxy.GetAppointmentsAsync();
            if (list != null)
            {
                allAppointments = list;
            }
            else
                allAppointments = new List<AppointmentModels>();

            SelectedStatus = AppStatuses[0];
            
        }
        public Command OnApproveCommand { get; set; }
        public Command OnDeclineCommand { get; set; }

        private async void OnApprove(AppointmentModels appointment)
        {
            appointment.AppointmentStatusId = 1;
            await proxy.UpdateAppointmentStatusAsync(appointment);
            OnStatusChange();
        }

        private async void OnDecline(AppointmentModels appointment)
        {
            appointment.AppointmentStatusId = 2;
            await proxy.UpdateAppointmentStatusAsync(appointment);
            OnStatusChange();
        }

        private void OnStatusChange()
        {
            Appointment.Clear();
            foreach(var a in allAppointments)
            {
                if (a.AppointmentStatusId == SelectedStatus.StatusId)
                    Appointment.Add(a);
            }

        }
        private List<AppointmentModels> allAppointments;
        private ObservableCollection<AppointmentModels> appointment;
        public ObservableCollection<AppointmentModels> Appointment
        {
            get => appointment;
            set
            {
                if (appointment != value)
                {
                    appointment = value;
                    OnPropertyChanged(nameof(Appointment));
                }
            }
        }

        private ObservableCollection<AppointmentStatusModels> appStatuses;
        public ObservableCollection<AppointmentStatusModels> AppStatuses
        {
            get => appStatuses;
            set
            {
                if (appStatuses != value)
                {
                    appStatuses = value;
                    OnPropertyChanged(nameof(AppStatuses));
                }
            }
        }

        private AppointmentStatusModels selectedStatus;
        public AppointmentStatusModels SelectedStatus
        {
            get => selectedStatus; 
            set
            {
                if (selectedStatus != value)
                {
                    selectedStatus = value;
                    OnStatusChange();
                    OnPropertyChanged(nameof(SelectedStatus));
                }
            }
        }



    }
}
