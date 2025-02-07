using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using System.Threading.Tasks;

namespace To_Do_app.Pages
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;

        [ObservableProperty]
        private string _welcomeText;

        public HomePageViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
            LoadUser();
        }

        private void LoadUser()
        {
            var user = _authClient.User;
            if (user != null)
            {
                WelcomeText = $"Welcome, {user.Info.DisplayName}!";
            }
            else
            {
                WelcomeText = "Welcome!";
            }
        }

        [RelayCommand]
        private async Task Logout()
        {
            _authClient.SignOut();
            await Shell.Current.GoToAsync("//SignIn");
        }
    }
}
