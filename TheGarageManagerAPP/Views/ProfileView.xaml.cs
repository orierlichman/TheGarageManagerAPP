using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModels vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}