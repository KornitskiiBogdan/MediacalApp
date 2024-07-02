using System;
using System.Reactive.Linq;
using MedicalApp.ViewModels;
using ReactiveUI;

namespace MedicalApp.Tools
{
    public static class AnalysisViewModelExtensions
    {
        public static IObservable<Func<MarkViewModel, bool>> GetObservableFilter(this AnalysisViewModel analysisViewModel)
        {
            return analysisViewModel
                .WhenAnyValue(x => x.SearchText)
                .Select(filter => new Func<MarkViewModel, bool>(mark =>
                    mark.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
