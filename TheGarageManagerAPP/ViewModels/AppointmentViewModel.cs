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
        }


        //private ObservableCollection<AppointmentModels> xxxx;
        //public ObservableCollection<AppointmentModels> Xxxx
        //{
        //    get => xxxx;
        //    set
        //    {
        //        if (xxxx != value)
        //        {
        //            xxxx = value;
        //            OnPropertyChanged(nameof(Xxxx));
        //        }
        //    }
        //}



        private int selectedStatus;
        public int SelectedStatus
        {
            get => selectedStatus; 
            set
            {
                if (selectedStatus != value)
                {
                    selectedStatus = value;
                    OnPropertyChanged(nameof(SelectedStatus));
                }
            }
        }


        private List<AppointmentStatusModels> selectedStatusModels;
        private List<AppointmentModels> selectedAppointmentModels;

    }
}
