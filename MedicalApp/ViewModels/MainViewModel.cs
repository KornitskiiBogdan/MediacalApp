using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MedicalApp.Messages;
using MedicalApp.Models;
using MedicalApp.ViewModels.Analysis;
using MedicalDatabase;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Tools.Messaging;

namespace MedicalApp.ViewModels;

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
        Project.MessageBus.Register<LoginSuccess>(_ =>
        {
            SelectedListItem = Items.FirstOrDefault(x => x.ModelType == typeof(AnalysisViewModel));
        });

        _currentPage = Project.Services.GetRequiredService<AnalysisViewModel>();

        SelectedListItem = Items.FirstOrDefault(x => x.ModelType == typeof(AnalysisViewModel));

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

            this.RaiseAndSetIfChanged(ref _selectedListItem, value);

            try
            {
                var vm = Project.Services.GetRequiredService(value.ModelType);
                if (vm is not ViewModelBase vmb)
                {
                    return;
                }

                CurrentPage = vmb;
            }
            catch(InvalidOperationException e)
            {
                Debug.WriteLine(e);
            }
            
        }
    }

    public ObservableCollection<ListItemTemplate> Items { get; }
}
