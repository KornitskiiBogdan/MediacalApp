using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MedicalApp.Messages;
using MedicalApp.Models;
using MedicalApp.ViewModels.Analysis;
using MedicalApp.ViewModels.Tabs;
using MedicalDatabase;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Tools.Messaging;

namespace MedicalApp.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private readonly Router _router;
    private ViewModelTabBase _currentPage;
    private ListItemTemplate? _selectedListItem;
    
    public override MedicalProject Project { get; }

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
            SelectedListItem = Items.FirstOrDefault(x => x.ModelType == typeof(AnalysisViewModelTab));
        });

        _currentPage = Project.Services.GetRequiredService<AnalysisViewModelTab>();
        _currentPage.ChangeCurrentTabEvent += CurrentPageOnChangeCurrentTabEvent;
        SelectedListItem = Items.FirstOrDefault(x => x.ModelType == typeof(AnalysisViewModelTab));

        //_currentPage = new LoginViewModel(Project, Project.Services.GetRequiredService<ILoginService>());
    }

    private void CurrentPageOnChangeCurrentTabEvent(ViewModelTabBase obj)
    {
        CurrentPage = obj;
    }

    public ViewModelTabBase CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage.ChangeCurrentTabEvent -= CurrentPageOnChangeCurrentTabEvent;
            this.RaiseAndSetIfChanged(ref _currentPage, value);
            value.ChangeCurrentTabEvent += CurrentPageOnChangeCurrentTabEvent;
        }
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
                if (vm is not ViewModelTabBase vmtb)
                {
                    return;
                }

                CurrentPage = vmtb;
            }
            catch(InvalidOperationException e)
            {
                Debug.WriteLine(e);
            }
            
        }
    }

    public ObservableCollection<ListItemTemplate> Items { get; }
}
