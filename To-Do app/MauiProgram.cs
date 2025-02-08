using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Extensions.Logging;
using To_Do_app.Pages;

namespace To_Do_app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyAC42MVEzGxammhTgrwipGVdnIQFPlIgIg",
                AuthDomain = "to-do-7265f.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                },
                UserRepository = new FileUserRepository("To-Do app"),
            }));

            builder.Services.AddSingleton<SignInViewModel>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<ProfileViewModel>();
            builder.Services.AddSingleton<AddTaskViewModel>();
            builder.Services.AddSingleton<AddTaskView>();
            builder.Services.AddSingleton<ProfileView>();
            builder.Services.AddSingleton<HomePageView>();
            builder.Services.AddSingleton<SignInView>();
            builder.Services.AddSingleton<SignUpView>();
            return builder.Build();
        }
    }
}
