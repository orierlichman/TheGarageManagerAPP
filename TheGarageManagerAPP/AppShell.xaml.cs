using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}
