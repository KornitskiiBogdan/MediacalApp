using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Collections.ObjectModel;
using MediacalApp.Models;
using MediacalApp.Attributes;
using DynamicData;
using DynamicData.Binding;

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

        public AnalysisViewModel(MedicalProject project)
        {
            Model = new AnalysisModel();
            _sourceListMark
                .Connect()
                .Sort(Model.Comparer, comparerChanged: this.WhenAnyValue(x => x.Model.Comparer))
                .Bind(out var newCollection)
                .Subscribe();

            Analysis = newCollection;
            _project = project;

             MarkViewModel mark = new MarkViewModel("Глюкоза", DateTime.FromBinary(2375698327), 15,
                "мг/л", 20, 10);
             _sourceListMark.Add(mark);
            MarkViewModel mark1 = new MarkViewModel("Целлюлоза", DateTime.FromBinary(8725698272), 25,
                "мг/л", 20, 10);
            _sourceListMark.Add(mark1);
            MarkViewModel mark2 = new MarkViewModel("Амминокислота", DateTime.FromBinary(5982350982), 20,
                "мг/л", 20, 10);
            _sourceListMark.Add(mark2);
        }

        public AnalysisModel Model { get; }

        public MarkViewModel? CurrentMark
        {
            get => _currentMark;
            set => this.RaiseAndSetIfChanged(ref _currentMark, value);
        }
        
    }
}
