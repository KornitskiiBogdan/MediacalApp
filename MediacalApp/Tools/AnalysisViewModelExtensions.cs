using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using MediacalApp.ViewModels;
using ReactiveUI;

namespace MediacalApp.Tools
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
