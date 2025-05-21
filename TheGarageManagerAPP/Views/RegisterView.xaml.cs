using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP.Views;

public partial class RegisterView : ContentPage
{
    public RegisterView(RegisterViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}
