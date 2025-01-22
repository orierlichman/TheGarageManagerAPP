using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using TasksManagementApp.Services;
using TheGarageManagerApp.Services;
using TheGarageManagerAPP.ViewModels;
using TheGarageManagerAPP.Views;

namespace TheGarageManagerAPP
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterDataServices()
                .RegisterPages()
                .RegisterViewModels();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }


        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<AllVehiclesView>();
            builder.Services.AddTransient<AppointmentView>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<CarRepairView>();
            builder.Services.AddTransient<PartsView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<TheGarageHomaPageView>();
            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<TheGarageManagerWebAPIProxy>();
            builder.Services.AddSingleton<GarageFinderServiceProxy>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<AllVehiclesViewModel>();
            builder.Services.AddTransient<AppointmentViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<CarRepairViewModel>();
            builder.Services.AddTransient<PartsViewModels>();
            builder.Services.AddTransient<ProfileViewModels>();
            builder.Services.AddTransient<TheGarageHomePageViewModel>();
            return builder;
        }


    }
}
