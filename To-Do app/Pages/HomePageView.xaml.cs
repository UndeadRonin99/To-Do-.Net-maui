using CommunityToolkit.Mvvm.ComponentModel;
using To_Do_app.Services;

namespace To_Do_app.Pages;

public partial class HomePageView : ContentPage
{

    private readonly FirebaseService _firebaseService;


    public HomePageView(HomePageViewModel model)
	{
		InitializeComponent();

		BindingContext = model;
        _firebaseService = new FirebaseService();
	}

    private async void OnTaskCompleted(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        var task = (Item)checkBox.BindingContext;

        if (checkBox.IsChecked)
        {
            // Update the task to mark it as completed
            task.IsCompleted = true;
            var isUpdated = await _firebaseService.UpdateTask(task);


        }
    }
}