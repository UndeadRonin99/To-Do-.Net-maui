using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using To_Do_app.Services;

namespace To_Do_app.Pages
{
    public partial class AddTaskViewModel : ObservableObject
    {
        private readonly FirebaseService _firebaseService;
        private readonly FirebaseAuthClient _authClient;
        private string uid;


        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private DateTime deadline = DateTime.Now;

        public ICommand AddToDoCommand { get; }

        public AddTaskViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
            LoadUser();
            _firebaseService = new FirebaseService();
            AddToDoCommand = new AsyncRelayCommand(AddToDoItem);
        }

        private void LoadUser()
        {
            uid = _authClient.User.Info.Uid;
        }

        private async Task AddToDoItem()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Shell.Current.DisplayAlert("Error", "Title is required.", "OK");
                return;
            }

            var newToDo = new Item
            {
                Name = Title,
                Description = Description,
                Deadline = Deadline,
                CreatedAt = DateTime.Now,
                UID = uid
            };

            bool success = await _firebaseService.AddToDoItem(newToDo);

            if (success)
            {
                await Shell.Current.DisplayAlert("Success", "To-Do added!", "OK");
                Title = string.Empty;
                Description = string.Empty;
                Deadline = DateTime.Now;
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to add To-Do.", "OK");
            }
        }
    }
}
