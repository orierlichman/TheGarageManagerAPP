using TheGarageManagerAPP.ViewModels;

namespace TheGarageManagerAPP.Views;

public partial class AllVehiclesView : ContentPage
{
	public AllVehiclesView(AllVehiclesViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        // Handle the text changed event here
        var searchBar = sender as SearchBar;
        var searchText = searchBar?.Text; // Use null-conditional operator

        if (BindingContext is AllVehiclesViewModel viewModel)
        {
            viewModel.SearchText = searchText ?? string.Empty; // Assign an empty string if searchText is null
            viewModel.SearchTextChangedCommand.Execute(null);
        }
    }

}