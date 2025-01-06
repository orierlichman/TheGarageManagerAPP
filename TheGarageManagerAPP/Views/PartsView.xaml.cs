namespace TheGarageManagerAPP.Views;

using TheGarageManagerAPP.ViewModels;

public partial class PartsView : ContentPage
{
	public PartsView(PartsViewModels vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}