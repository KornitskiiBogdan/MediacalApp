using System.Collections.ObjectModel;
using DynamicData;
using MedicalApp.Attributes;
using MedicalApp.Models;
using MedicalApp.ViewModels.Interfaces;
using MedicalDatabase;
using PDFReader;
using ReactiveUI;
using SkiaSharp;
using System;

namespace MedicalApp.ViewModels.Documents
{
    [MedicalExtension]
    public class DocumentsViewModel : ViewModelBase, IFilteredObject
    {
        private readonly MedicalProject _medicalProject;
        private readonly PdfReader _pdfReader;
        private SKBitmap? _currentDocument;
        private readonly SortingModel _sortingModel;
        private readonly SourceList<DocumentViewModel> _pdfDocuments = new();

        public DocumentsViewModel(MedicalProject project)
        {
            _medicalProject = project;

            _sortingModel = new SortingModel();
            _pdfReader = new PdfReader();

            _pdfDocuments
                .Connect()
                .Filter(this.GetObservableFilter())
                .Sort(SortingModel.Comparer, comparerChanged: this.WhenAnyValue(x => x.SortingModel.Comparer))
                .Bind(out var newCollection)
                .Subscribe();

            PdfDocuments = newCollection;

            //_pdfDocuments = new ObservableCollection<SKBitmap> { _pdfReader.GeBitmapFromPdf() };
        }


        public SKBitmap? CurrentDocument
        {
            get => _currentDocument;
            set => this.RaiseAndSetIfChanged(ref _currentDocument, value);
        }

        public ReadOnlyObservableCollection<DocumentViewModel> PdfDocuments { get; set; }

        public string SearchText { get; set; }

        public SortingModel SortingModel => _sortingModel;
    }
}
