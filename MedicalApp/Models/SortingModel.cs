using System.Collections.Generic;
using MedicalApp.Tools;
using MedicalApp.ViewModels.Interfaces;
using ReactiveUI;

namespace MedicalApp.Models
{
    public class SortingModel : ReactiveObject
    {
        private IComparer<ISortedObject> _comparer = new ComparerObjectByName();

        public IComparer<ISortedObject> Comparer
        {
            get => _comparer;
            set => this.RaiseAndSetIfChanged(ref _comparer, value);
        }

        public void SortByDate()
        {
            Comparer = new ComparerByDate();
        }

        public void SortByCategory()
        {

        }

        public void SortByOrder()
        {
            Comparer = new ComparerObjectByName();
        }
    }
}
