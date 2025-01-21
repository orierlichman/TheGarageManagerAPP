using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP.Views;

public partial class TheGarageHomaPageView : ContentPage
{
	public TheGarageHomaPageView(TheGarageHomePageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}