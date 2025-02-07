using CommunityToolkit.Mvvm.ComponentModel;

namespace To_Do_app.Pages;

public partial class HomePageView : ContentPage
{

    public HomePageView(HomePageViewModel model)
	{
		InitializeComponent();

		BindingContext = model;
	}
}