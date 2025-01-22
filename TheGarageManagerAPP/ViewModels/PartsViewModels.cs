using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.Models;

namespace TheGarageManagerAPP.ViewModels
{
    public class PartsViewModels : ViewModelBase
    {
        ////This is a List containing all of the garage parts
        //private List<GaragePartsModels> garagePartsModels;

        private string _searchText;

        private TheGarageManagerWebAPIProxy proxy;

        private IServiceProvider serviceProvider;

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

        public ICommand SearchTextChangedCommand { get; }


        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchTextChangedCommand.Execute(null);
            }
        }


        public PartsViewModels(TheGarageManagerWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            FillAllParts();
            SearchTextChangedCommand = new Command(async () => await SearchParts());

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


        private async Task SearchParts()
        {
            var parts = await proxy.GetAllGaragePartsAsync();
            if (parts != null)
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    GarageParts = new ObservableCollection<GaragePartsModels>(parts);
                }
                else
                {
                    GarageParts = new ObservableCollection<GaragePartsModels>(
                        parts.FindAll(g => g.PartName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
                }
            }
        }





    }
}
