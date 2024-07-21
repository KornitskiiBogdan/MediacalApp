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
using MedicalApp.Messages;
using Tools.Messaging;

namespace MedicalApp.ViewModels.Documents
{
    [MedicalExtension]
    public class DocumentsViewModel : ViewModelBase, IFilteredObject
    {
        private readonly PdfReader _pdfReader;
        private DocumentViewModel? _currentDocument;
        private readonly SortingModel _sortingModel;
        private readonly MedicalProject _medicalProject;
        private readonly SourceList<DocumentViewModel> _pdfDocuments = new();
        private string _searchText = "";

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

            project.MessageBus.Register<GoBackView>(backView =>
            {
                if (backView.TypeView == typeof(DocumentViewModel))
                {
                    CurrentDocument = null;
                }

            });

            _pdfDocuments.Add(new DocumentViewModel(project, _pdfReader.GetBitmapFromPdf(), "анализ"));
        }


        public DocumentViewModel? CurrentDocument
        {
            get => _currentDocument;
            set => this.RaiseAndSetIfChanged(ref _currentDocument, value);
        }

        public ReadOnlyObservableCollection<DocumentViewModel> PdfDocuments { get; set; }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public SortingModel SortingModel => _sortingModel;
    }
}
