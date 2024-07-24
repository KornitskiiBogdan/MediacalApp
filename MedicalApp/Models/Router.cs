using System;
using Authentication;
using MedicalApp.ViewModels;
using MedicalApp.ViewModels.Analysis;
using MedicalApp.ViewModels.Documents;
using MedicalApp.ViewModels.Tabs;
using MedicalDatabase;
using MedicalDatabase.Operations;
using Microsoft.Extensions.DependencyInjection;
using Tools.Messaging;

namespace MedicalApp.Models
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
                var repository = new MedicalRepository();
                openedApp.Services.AddSingleton<MedicalRepository>(repository);
                openedApp.Services.AddSingleton<MedicalProject>(openedApp.Project);
                AddMainTabs(openedApp.Services, openedApp.Project);
                openedApp.Services.AddHttpClient<ILoginService, LoginService>(httpClient =>
                    httpClient.BaseAddress = new Uri("https://dummyjson.com/"));
            });
        }

        private void AddMainTabs(IServiceCollection serviceCollection, MedicalProject project)
        {
            serviceCollection.AddSingleton<SettingsViewModel>(new SettingsViewModel(project));
            serviceCollection.AddSingleton<AnalysisViewModelTab>(new AnalysisViewModelTab(new AnalysisViewModel(project)));
            serviceCollection.AddSingleton<DocumentsViewModelTab>(new DocumentsViewModelTab(new DocumentsViewModel(project)));
            serviceCollection.AddSingleton<DocumentsViewModel>(new DocumentsViewModel(project));
            serviceCollection.AddSingleton<ProfileViewModel>(new ProfileViewModel(project));
        }
    }
}
