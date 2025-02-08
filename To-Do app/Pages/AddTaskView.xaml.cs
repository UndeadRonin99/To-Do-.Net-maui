namespace To_Do_app.Pages;

public partial class AddTaskView : ContentPage
{
	public AddTaskView(AddTaskViewModel model)
	{
        CurrentTime = DateTime.Now;
		InitializeComponent();
		BindingContext = model;
	}

    public DateTime CurrentTime { get; set; } // The properties that the page binds to need to have the same name as the properties in the model

}