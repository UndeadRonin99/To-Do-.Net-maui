namespace To_Do_app.Pages;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel model)
	{
		InitializeComponent();
        BindingContext = model;

    }
}