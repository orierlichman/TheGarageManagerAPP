using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP.Views;

public partial class AppointmentView : ContentPage
{
	public AppointmentView(AppointmentViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}