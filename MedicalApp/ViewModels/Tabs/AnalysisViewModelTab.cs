using MedicalApp.ViewModels.Analysis;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.ViewModels.Tabs
{
    public class AnalysisViewModelTab : ViewModelTabBase
    {
        private string _header;
        private MarkViewModel? _currentMark;
        private AnalysisViewModel _viewModel;

        public AnalysisViewModelTab(AnalysisViewModel viewModel)
        {
            _header = "Анализы";
            _viewModel = viewModel;
        }

        public override string Header
        {
            get => _header;
            set => _header = value;
        }

        public AnalysisViewModel ViewModel
        {
            get => _viewModel;
            set => _viewModel = value;
        }

        public MarkViewModel? CurrentMark
        {
            get => _currentMark;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentMark, value);
                if (value != null)
                {
                    ChangeCurrentTabInvoke(new MarkViewModelTab(value));
                }
            }
        }

        public override void Dispose()
        {
            
        }
    }
}
