using System.Collections.ObjectModel;
using MedicalApp.Attributes;
using MedicalDatabase;
using PDFReader;
using ReactiveUI;
using SkiaSharp;

namespace MedicalApp.ViewModels
{
    [MedicalExtension]
    public class DocumentsViewModel : ViewModelBase
    {
        private readonly MedicalProject _medicalProject;
        private readonly PdfReader _pdfReader;
        private SKBitmap? _currentDocument;
        private ObservableCollection<SKBitmap> _pdfDocuments;

        public DocumentsViewModel(MedicalProject project)
        {
            _medicalProject = project;
            _pdfReader = new PdfReader();
            PdfDocuments = new ObservableCollection<SKBitmap> { _pdfReader.GeBitmapFromPdf() };
        }


        public SKBitmap? CurrentDocument
        {
            get => _currentDocument;
            set => this.RaiseAndSetIfChanged(ref _currentDocument, value);
        }

        public ObservableCollection<SKBitmap> PdfDocuments
        {
            get => _pdfDocuments;
            set => this.RaiseAndSetIfChanged(ref _pdfDocuments, value);
        }
    }
}
