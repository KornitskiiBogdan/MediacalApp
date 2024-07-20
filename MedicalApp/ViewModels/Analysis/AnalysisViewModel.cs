using System;
using System.Collections.ObjectModel;
using DynamicData;
using MedicalApp.Attributes;
using MedicalApp.Messages;
using MedicalApp.Models;
using MedicalApp.Tools;
using MedicalApp.ViewModels.Interfaces;
using MedicalDatabase;
using MedicalDatabase.Operations;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Tools.Messaging;

namespace MedicalApp.ViewModels.Analysis
{
    [MedicalExtension]
    public class AnalysisViewModel : ViewModelBase, IFilteredObject
    {
        private MarkViewModel? _currentMark;
        private readonly MedicalProject _project;
        private readonly SourceList<MarkViewModel> _sourceListMark = new();
        private readonly SortingModel _sortingModel;
        private string _searchText = "";

        public AnalysisViewModel(MedicalProject project)
        {
            _sortingModel = new SortingModel();

            _sourceListMark
                .Connect()
                .Filter(this.GetObservableFilter())
                .Sort(SortingModel.Comparer, comparerChanged: this.WhenAnyValue(x => x.SortingModel.Comparer))
                .Bind(out var newCollection)
                .Subscribe();

            Analysis = newCollection;
            _project = project;

            _project.MessageBus.Register<GoBackView>(backView =>
            {
                if (backView.TypeView == typeof(MarkViewModel))
                {
                    CurrentMark = null;
                }

            });

            _project.MessageBus.Register<ServiceCreationCompleted>(_ => Init());

        }

        public void Init()
        {
            foreach (var mark in _project.Services.GetRequiredService<MedicalRepository>().Reader.ReadMarks())
            {
                _sourceListMark.Add(new MarkViewModel(mark, _project));
            }
        }

        public ReadOnlyObservableCollection<MarkViewModel> Analysis { get; set; }

        public SortingModel SortingModel => _sortingModel;

        public MarkViewModel? CurrentMark
        {
            get => _currentMark;
            set => this.RaiseAndSetIfChanged(ref _currentMark, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }
    }
}
