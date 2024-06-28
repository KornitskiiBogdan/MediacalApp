using System;
using MediacalApp.Messaging;
using MediacalApp.Messaging.Messages;
using MediacalApp.Service.LoginService;
using MediacalApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MediacalApp.Models
{
    public class Router
    {
        private readonly MainViewModel _mainViewModel;
        public Router(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            MessageBus = StrongReferenceMessageBus.Instance;

            Register();
        }

        public IMessageBus MessageBus { get; }

        public void Register()
        {
            MessageBus.Register<OpenedApp>(openedApp =>
            {
                openedApp.Services.AddSingleton<MedicalProject>(openedApp.Project);
                openedApp.Services.AddSingleton<SettingsViewModel>(new SettingsViewModel(openedApp.Project));
                openedApp.Services.AddSingleton<AnalysisViewModel>(new AnalysisViewModel(openedApp.Project));
                openedApp.Services.AddSingleton<DocumentsViewModel>(new DocumentsViewModel(openedApp.Project));
                openedApp.Services.AddSingleton<ProfileViewModel>(new ProfileViewModel(openedApp.Project));
                openedApp.Services.AddHttpClient<ILoginService, LoginService>(httpClient =>
                    httpClient.BaseAddress = new Uri("https://dummyjson.com/"));
            });
        }
    }
}
