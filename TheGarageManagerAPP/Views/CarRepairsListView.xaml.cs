using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP.Views;

public partial class CarRepairsListView : ContentPage
{
	public CarRepairsListView(CarRepairsList vm)
	{
		this.BindingContext = vm;
        InitializeComponent();
	}
}