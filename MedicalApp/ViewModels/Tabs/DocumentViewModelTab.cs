using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalApp.ViewModels.Analysis;
using MedicalApp.ViewModels.Documents;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace MedicalApp.ViewModels.Tabs
{
    public class DocumentViewModelTab : ViewModelTabBase
    {
        private string _header;
        private DocumentViewModel _viewModel;

        public DocumentViewModelTab(DocumentViewModel viewModel)
        {
            _header = "Документы";
            _viewModel = viewModel;
        }

        public override string Header
        {
            get => _header;
            set => this.RaiseAndSetIfChanged(ref _header, value);
        }

        public DocumentViewModel ViewModel
        {
            get => _viewModel;
            set => this.RaiseAndSetIfChanged(ref _viewModel, value);
        }

        public void GoBackCommand()
        {
            ChangeCurrentTabInvoke(_viewModel.Project.Services.GetRequiredService<DocumentsViewModelTab>());
        }

        public override void Dispose()
        {
            
        }
    }
}
