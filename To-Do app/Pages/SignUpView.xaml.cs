namespace To_Do_app.Pages;

public partial class SignUpView : ContentPage
{
	public SignUpView(SignUpViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}