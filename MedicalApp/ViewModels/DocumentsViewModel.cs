using System.Collections.ObjectModel;
using MedicalApp.Attributes;
using MedicalDatabase;
using PDFReader;
using SkiaSharp;

namespace MedicalApp.ViewModels
{
    [MedicalExtension]
    public class DocumentsViewModel : ViewModelBase
    {
        private readonly MedicalProject _medicalProject;
        private readonly PdfReader _pdfReader;

        public DocumentsViewModel(MedicalProject project)
        {
            _medicalProject = project;
            _pdfReader = new PdfReader();
            PdfDocuments = new ObservableCollection<SKBitmap> { _pdfReader.GeBitmapFromPdf() };
        }

        public SKBitmap? CurrentDocument { get; set; }

        public ObservableCollection<SKBitmap> PdfDocuments { get; set; }
    }
}
