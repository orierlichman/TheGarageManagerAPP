using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP.Views;

public partial class CarRepairView : ContentPage
{
	public CarRepairView(CarRepairViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}