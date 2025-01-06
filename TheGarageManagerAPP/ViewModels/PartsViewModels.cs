using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Models;

namespace TheGarageManagerAPP.ViewModels
{
    public class PartsViewModels : ViewModelBase
    {
        //#region collection view 
        //private ObservableCollection<GaragePartsModels> garageParts;
        //public ObservableCollection<GaragePartsModels> GarageParts
        //{
        //    get => garageParts;
        //    set
        //    {
        //        garageParts = value;
        //        OnPropertyChanged(nameof(GarageParts));
        //    }
        //}

        //#endregion


        //private TheGarageManagerWebAPIProxy proxy;
        //private IServiceProvider serviceProvider;
        //public PartsViewModels(TheGarageManagerWebAPIProxy proxy, IServiceProvider serviceProvider)
        //{
        //    this.proxy = proxy;
        //    this.serviceProvider = serviceProvider;
        //    FillAllParts();

        //}



        //#region get all parts
        //// fill the observable collection with all the parts
        //public async void FillAllParts()
        //{
        //    List<GaragePartsModels> parts = new List<GaragePartsModels>();
        //    parts = await GetAllParts();
        //    GarageParts = new ObservableCollection<GaragePartsModels>(parts);
        //}

        //public async Task<List<GaragePartsModels>> GetAllParts()
        //{
        //    List<GaragePartsModels> list = await this.proxy.GetAllGaragePartsAsync();
        //    return list;
        //}
        //#endregion









        private ObservableCollection<GaragePartsModels> garageParts;
        public ObservableCollection<GaragePartsModels> GarageParts
        {
            get => garageParts;
            set
            {
                garageParts = value;
                OnPropertyChanged(nameof(GarageParts));
            }
        }

        private TheGarageManagerWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public PartsViewModels(TheGarageManagerWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            FillAllParts();
        }

        public async void FillAllParts()
        {
            List<GaragePartsModels> parts = await GetAllParts();
            GarageParts = new ObservableCollection<GaragePartsModels>(parts);
        }

        public async Task<List<GaragePartsModels>> GetAllParts()
        {
            List<GaragePartsModels> list = await this.proxy.GetAllGaragePartsAsync();
            return list;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }














































        #region properties

        ////Search bar text
        //private string searchText;
        //public string SearchText
        //{
        //    get => searchText;
        //    set
        //    {
        //        searchText = value;
        //        ///*FilterTasks*/();
        //        OnPropertyChanged();
        //    }
        //}


        #endregion


        ////this method filter the tasks based on the search text and the show done and show not done tasks
        //private void FilterTasks()
        //{
        //    List<UrgencyLevel> urgencyLevels = ((App)Application.Current).UrgencyLevels;
        //    filteredUserTasks.Clear();
        //    //Sort the tasks by urgency level
        //    userTasks.OrderByDescending(t => t.UrgencyLevelId);

        //    foreach (var task in userTasks)
        //    {
        //        if ((task.TaskActualDate.HasValue && this.showDoneTasks || !task.TaskActualDate.HasValue && this.showNotDoneTasks) &&
        //            (task.TaskDescription.Contains(SearchText) || string.IsNullOrEmpty(SearchText)))
        //        {
        //            string urgency = urgencyLevels.Where(u => u.UrgencyLevelId == task.UrgencyLevelId).FirstOrDefault().UrgencyLevelName;
        //            FilteredUserTasks.Add(new TaskDisplay()
        //            {
        //                Id = task.TaskId,
        //                Description = task.TaskDescription,
        //                Urgency = urgency,
        //                DueDate = task.TaskDueDate,
        //                ActualDate = task.TaskActualDate
        //            });
        //        }
        //    }

        //}
    }
}
