using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}