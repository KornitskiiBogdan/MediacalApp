using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.ViewModels.Interfaces
{
    public interface IFilteredObject : IReactiveObject
    {
        public string SearchText { get; set; }
    }

    public static class FilteredObjectExtensions
    {
        public static IObservable<Func<INamedObject, bool>> GetObservableFilter(this IFilteredObject filteredObject)
        {
            return filteredObject
                .WhenAnyValue(x => x.SearchText)
                .Select(filter => new Func<INamedObject, bool>(mark =>
                    mark.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
