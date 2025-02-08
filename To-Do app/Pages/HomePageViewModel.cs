using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using To_Do_app.Services;

namespace To_Do_app.Pages
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;
        private readonly FirebaseService _firebaseService;
        private string UID;

        [ObservableProperty]
        private ObservableCollection<Item> tasks = new();

        public ICommand LoadTasksCommand { get; }

        public HomePageViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
            LoadUser();
             _firebaseService = new FirebaseService();
        LoadTasksCommand = new AsyncRelayCommand(LoadUserTasks);
        }

        private void LoadUser()
        {
            var user = _authClient.User;
            UID = user.Uid;
        }

        private async Task LoadUserTasks()
        {
            var userTasks = await _firebaseService.GetUserTasks(UID);
            Tasks = new ObservableCollection<Item>(userTasks);
        }

       
    }
}

