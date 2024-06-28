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
using MediacalApp.Messaging.Messages;
using MediacalApp.Tools;

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

             MarkViewModel mark = new MarkViewModel(new MarkModel(20, 10), _project, "Глюкоза", DateTime.FromBinary(2375698327), 15,
                "мг/л");
             _sourceListMark.Add(mark);
            MarkViewModel mark1 = new MarkViewModel(new MarkModel(20, 10), _project, "Целлюлоза", DateTime.FromBinary(8725698272), 25,
                "мг/л");
            _sourceListMark.Add(mark1);
            MarkViewModel mark2 = new MarkViewModel(new MarkModel(20, 10), _project, "Амминокислота", DateTime.FromBinary(5982350982), 20,
                "мг/л");
            _sourceListMark.Add(mark2);

            _project.MessageBus.Register<GoBackView>(backView =>
            {
                CurrentMark = null;
            });

            _project.MessageBus.Register<GoNextView>(backView =>
            {
                if (CurrentMark == null)
                {
                    return;
                }

                var arrayItems = _sourceListMark.Items.ToArray();
                var indexCurrentMark = arrayItems.IndexOf(CurrentMark);
                if (indexCurrentMark == arrayItems.Length - 1)
                {
                    CurrentMark = arrayItems[0];
                }
                else
                {
                    CurrentMark = arrayItems[indexCurrentMark + 1];
                }
            });
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
