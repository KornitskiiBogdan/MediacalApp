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
using MedicalDatabase.Operations;
using Microsoft.Extensions.DependencyInjection;
using Tools.Messaging;
using MedicalApp.ViewModels.Analysis;

namespace MedicalApp.ViewModels.Documents
{
    [MedicalExtension]
    public class DocumentsViewModel : ViewModelBase, IFilteredObject
    {
        private readonly SortingModel _sortingModel;
        private readonly MedicalProject _medicalProject;
        private readonly SourceList<DocumentViewModel> _pdfDocuments = new();
        private string _searchText = "";

        public DocumentsViewModel(MedicalProject project)
        {
            _medicalProject = project;

            _sortingModel = new SortingModel();

            _pdfDocuments
                .Connect()
                .Filter(this.GetObservableFilter())
                .Sort(SortingModel.Comparer, comparerChanged: this.WhenAnyValue(x => x.SortingModel.Comparer))
                .Bind(out var newCollection)
                .Subscribe();

            PdfDocuments = newCollection;

            _medicalProject.MessageBus.Register<ServiceCreationCompleted>(_ => Init());

            
        }

        public void Init()
        {
            var databaseDocuments = _medicalProject.Services.GetRequiredService<MedicalRepository>().Reader.ReadDocuments();
            foreach (var databaseDocument in databaseDocuments)
            {
                _pdfDocuments.Add(new DocumentViewModel(_medicalProject, databaseDocument));
            }
        }

        public ReadOnlyObservableCollection<DocumentViewModel> PdfDocuments { get; set; }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public SortingModel SortingModel => _sortingModel;

        public override MedicalProject Project => _medicalProject;
    }
}
