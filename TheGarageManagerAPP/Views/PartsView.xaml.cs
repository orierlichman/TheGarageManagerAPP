namespace TheGarageManagerAPP.Views;

using TheGarageManagerAPP.ViewModels;

public partial class PartsView : ContentPage
{
	public PartsView(PartsViewModels vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}


    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        // Handle the text changed event here
        var searchBar = sender as SearchBar;
        var searchText = searchBar?.Text; // Use null-conditional operator

        if (BindingContext is PartsViewModels viewModel)
        {
            viewModel.SearchText = searchText ?? string.Empty; // Assign an empty string if searchText is null
            viewModel.SearchTextChangedCommand.Execute(null);
        }
    }


}