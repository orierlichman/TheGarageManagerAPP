﻿using TheGarageManagerAPP.ViewModels;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("theGaragesHomePageView", typeof(TheGarageHomaPageView));
            Routing.RegisterRoute("llVehiclesView", typeof(AllVehiclesView));
            Routing.RegisterRoute("updateProfileView", typeof(ProfileView));
            Routing.RegisterRoute("CarRepair", typeof(CarRepairView));
        }

    }
}
