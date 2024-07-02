using System.Collections.Generic;
using MedicalApp.Tools;
using MedicalApp.ViewModels;
using ReactiveUI;

namespace MedicalApp.Models
{
    public class AnalysisModel : ReactiveObject
    {
        private IComparer<MarkViewModel> _comparer = new ComparerMarkByOrder();

        public IComparer<MarkViewModel> Comparer
        {
            get => _comparer;
            set => this.RaiseAndSetIfChanged(ref _comparer, value);
        }

        public void SortByDate()
        {
            Comparer = new ComparerMarkByDate();
        }

        public void SortByCategory()
        {

        }

        public void SortByOrder()
        {
            Comparer = new ComparerMarkByOrder();
        }
    }
}
