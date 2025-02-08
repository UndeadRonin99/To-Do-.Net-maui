using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_app.Pages
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;

        [ObservableProperty]
        private string _welcomeText;

        public ProfileViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
            LoadUser();
        }

        private void LoadUser()
        {
            var user = _authClient.User;
            if (user != null)
            {
                WelcomeText = $"Nice to see you here {user.Info.DisplayName}!";
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
