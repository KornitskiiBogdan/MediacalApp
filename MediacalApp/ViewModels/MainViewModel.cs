using System.Collections.ObjectModel;
using MediacalApp.Messaging;
using MediacalApp.Messaging.Messages;
using MediacalApp.Models;
using MediacalApp.Service.LoginService;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace MediacalApp.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private readonly Router _router;
    private ViewModelBase _currentPage;
    private ListItemTemplate? _selectedListItem;
    
    public MedicalProject Project { get; }

    public MainViewModel()
    {
        _router = new Router(this);
        Items = new ObservableCollection<ListItemTemplate>(Tools.ListItemTemplates.GetTemplates());

        var taskMedicalProject = MedicalProject.Create();
        if (!taskMedicalProject.IsCompleted)
        {
            taskMedicalProject.RunSynchronously();
        }
        Project = taskMedicalProject.Result;
        Project.MessageBus.Register<LoginSuccess>(loginSucces =>
        {
            CurrentPage = Project.Services.GetRequiredService<AnalysisViewModel>();
        });
        
        _currentPage = Project.Services.GetRequiredService<AnalysisViewModel>();
        //Пока так. Из-за циклических зависимостей, потому что одновременно добавляется и ILoginService и LoginViewModel.
        //Мб это норм, потому что окно логин нужно только при входе
        //Нужно подумать...
        //_currentPage = new LoginViewModel(Project, Project.Services.GetRequiredService<ILoginService>());

    }

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public ListItemTemplate? SelectedListItem
    {
        get => _selectedListItem;
        set
        {
            if (value == null)
            {
                return;
            }

            var vm = Project.Services.GetRequiredService(value.ModelType);
            if (vm is not ViewModelBase vmb)
            {
                return;
            }

            CurrentPage = vmb;

            this.RaiseAndSetIfChanged(ref _selectedListItem, value);
        }
    }

    public ObservableCollection<ListItemTemplate> Items { get; }
}
