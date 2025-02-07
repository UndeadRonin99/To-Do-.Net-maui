using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using Firebase.Auth.Providers;
using System.Text.RegularExpressions;

namespace To_Do_app.Pages
{
    public partial class SignInViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _authClient;

        public SignInViewModel(FirebaseAuthClient authClient)
        {
            _authClient = authClient;
            AttemptSignin();
        }

        public async Task AttemptSignin()
        {
            if (_authClient.User != null)
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
        }

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        public string Username => _authClient.User?.Info?.DisplayName;

        [RelayCommand]
        private async Task SignIn()
        {
            if (_authClient.User != null)
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
            else if(string.IsNullOrWhiteSpace(_email) || string.IsNullOrWhiteSpace(_password))
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                string text = "Please fill both fields";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            }
            else if (IsValidEmail(_email.Trim()))
            {
                try
                {
                    var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(Email.Trim(), Password);

                    if (userCredential != null) // ✅ Check if authentication was successful
                    {
                        await Shell.Current.GoToAsync("//HomePage");
                    }
                    else
                    {
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                        string text = "Authentication Error";
                        ToastDuration duration = ToastDuration.Short;
                        double fontSize = 14;

                        var toast = Toast.Make(text, duration, fontSize);

                        await toast.Show(cancellationTokenSource.Token);
                    }
                }
                catch (FirebaseAuthException ex)
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    string text = "Authentication Error";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 14;

                    var toast = Toast.Make(text, duration, fontSize);

                    await toast.Show(cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    string text = "Unexpected error please try again";
                    ToastDuration duration = ToastDuration.Short;
                    double fontSize = 14;

                    var toast = Toast.Make(text, duration, fontSize);

                    await toast.Show(cancellationTokenSource.Token);
                }
            }
            else
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                string text = "Please enter a valid email";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            }
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // ✅ Regular expression for email validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        [RelayCommand]
        private async Task NavigateSignUp()
        {
            await Shell.Current.GoToAsync("//SignUp");
        }
    }
}
