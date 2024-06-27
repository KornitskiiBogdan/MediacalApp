using System;
using MediacalApp.ViewModels.Charts;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MediacalApp.Models;
using MediacalApp.Attributes;

namespace MediacalApp.ViewModels
{
    [MedicalExtension]
    public class AnalysisViewModel : ViewModelBase
    {
        public ObservableCollection<MarkViewModel> Analysis
        {
            get => _analysis;
            set => _analysis = value;
        }

        //private ChartViewModel _chart = new LineChartViewModel();
        private ObservableCollection<MarkViewModel> _analysis = new ObservableCollection<MarkViewModel>();
        private MarkViewModel? _currentMark;
        private readonly MedicalProject _project;

        public AnalysisViewModel(MedicalProject project) 
        {
            _project = project;
            MarkViewModel mark = new MarkViewModel("Глюкоза", DateTime.Today.Date.ToString("D"), 15,
                "мг/л", 20, 10);
            Analysis.Add(mark);
            MarkViewModel mark1 = new MarkViewModel("Глюкоза", DateTime.Today.Date.ToString("D"), 25,
                "мг/л", 20, 10);
            Analysis.Add(mark1);
            MarkViewModel mark2 = new MarkViewModel("Глюкоза", DateTime.Today.Date.ToString("D"), 20,
                "мг/л", 20, 10);
            Analysis.Add(mark2);
        }

        public MarkViewModel CurrentMark
        {
            get => _currentMark;
            set => this.RaiseAndSetIfChanged(ref _currentMark, value);
        }

        //public ICommand SortByDate { get; set; } = new ReactiveCommand<TParam,TResult>()
    }
}
