using MedicalApp.ViewModels.Documents;
using ReactiveUI;

namespace MedicalApp.ViewModels.Tabs
{
    public class DocumentsViewModelTab : ViewModelTabBase
    {
        private string _header;
        private DocumentsViewModel _viewModel;
        private DocumentViewModel? _currentDocument;

        public DocumentsViewModelTab(DocumentsViewModel viewModel) : base(viewModel.Project.MessageBus)
        {
            _header = "Документы";
            _viewModel = viewModel;
        }

        public override string Header
        {
            get => _header;
            set => this.RaiseAndSetIfChanged(ref _header, value);
        }

        public DocumentsViewModel ViewModel
        {
            get => _viewModel;
            set => this.RaiseAndSetIfChanged(ref _viewModel, value);
        }

        public DocumentViewModel? CurrentDocument
        {
            get => _currentDocument;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentDocument, value);
                if (value != null)
                {
                    ChangeCurrentTab(new DocumentViewModelTab(value));
                }
            }
        }

        public override void Dispose()
        {
            
        }
    }
}
