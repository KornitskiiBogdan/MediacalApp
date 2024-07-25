using MedicalApp.ViewModels.Analysis;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace MedicalApp.ViewModels.Tabs
{
    public class MarkViewModelTab : ViewModelTabBase
    {
        private string _header;
        private MarkViewModel _viewModel;

        public MarkViewModelTab(MarkViewModel viewModel) : base(viewModel.Project.MessageBus)
        {
            _header = "Aнализы";
            _viewModel = viewModel;
        }

        public override string Header
        {
            get => _header;
            set => this.RaiseAndSetIfChanged(ref _header, value);
        }

        public MarkViewModel ViewModel
        {
            get => _viewModel;
            set => this.RaiseAndSetIfChanged(ref _viewModel, value);
        }

        public void GoBackCommand()
        {
            ChangeCurrentTab(_viewModel.Project.Services.GetRequiredService<AnalysisViewModelTab>());
        }

        public override void Dispose()
        {
            
        }
    }
}
