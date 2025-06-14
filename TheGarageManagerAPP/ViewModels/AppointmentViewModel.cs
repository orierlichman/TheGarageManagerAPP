﻿using System;
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
            RefreshCommand = new Command(Refresh);
            TransferAppointmentsCommand = new Command(async () => await TransferAppointments());
            Appointment = new ObservableCollection<AppointmentModels>();
            InitData();
        }

        public Command RefreshCommand { get; set; }
        public override void Refresh()
        {
            base.Refresh();
            InitData();
        }
        private async void InitData()
        {
            InServerCall = true;
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
            InServerCall = false;
        }
        public Command OnApproveCommand { get; set; }
        public Command OnDeclineCommand { get; set; }

        public Command TransferAppointmentsCommand { get; set; }


        private async Task TransferAppointments()
        {
            InServerCall = true;

            // קריאה לשירות שמעביר את התורים
            bool success = await proxy.TransferPendingAppointmentsAsync();

            if (success)
            {
                // טען מחדש את התורים למסך
                List<AppointmentModels>? list = await this.proxy.GetAppointmentsAsync();
                if (list != null)
                    allAppointments = list;
                else
                    allAppointments = new List<AppointmentModels>();

                OnStatusChange(); // כדי לסנן לפי סטטוס נבחר
            }

            InServerCall = false;
        }

        private async void OnApprove(AppointmentModels appointment)
        {
            await proxy.UpdateAppointmentStatusAsync(appointment.AppointmentID, 1);
            appointment.AppointmentStatusId = 1; // עדכון הסטטוס במודל
            OnStatusChange();
        }

        private async void OnDecline(AppointmentModels appointment)
        {
            await proxy.UpdateAppointmentStatusAsync(appointment.AppointmentID, 2);
            appointment.AppointmentStatusId = 2; // עדכון הסטטוס במודל
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
                    
                    OnPropertyChanged(nameof(SelectedStatus));
                }
                OnStatusChange();
            }
        }



    }
}
