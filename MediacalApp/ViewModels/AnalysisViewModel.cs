using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using MediacalApp.Models;
using MediacalApp.Attributes;
using DynamicData;
using MediacalApp.Messaging;
using MediacalApp.Tools;
using MediacalApp.Messages;
using MedicalDatabase;
using MedicalDatabase.Objects;
using MedicalDatabase.Operations;
using Microsoft.Extensions.DependencyInjection;
using LiveChartsCore.Geo;

namespace MediacalApp.ViewModels
{
    [MedicalExtension]
    public class AnalysisViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<MarkViewModel> Analysis { get; set; }

        //private ChartViewModel _chart = new LineChartViewModel();
        private MarkViewModel? _currentMark;
        private readonly MedicalProject _project;
        private readonly SourceList<MarkViewModel> _sourceListMark = new ();
        private readonly AnalysisModel _model;
        private string _searchText = "";

        public AnalysisViewModel(MedicalProject project)
        {
            _model = new AnalysisModel();

            _sourceListMark
                .Connect()
                .Filter(this.GetObservableFilter())
                .Sort(Model.Comparer, comparerChanged: this.WhenAnyValue(x => x.Model.Comparer))
                .Bind(out var newCollection)
                .Subscribe();

            Analysis = newCollection;
            _project = project;

            _project.MessageBus.Register<GoBackView>(backView =>
            {
                CurrentMark = null;
            });

            _project.MessageBus.Register<ServiceCreationCompleted>(_ => Init());

        }

        public void Init()
        {
            foreach (var mark in _project.Services.GetRequiredService<ReadFromDatabase>().ReadMarks())
            {
                _sourceListMark.Add(new MarkViewModel(mark, _project));
            }
        }

        public AnalysisModel Model => _model;

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
