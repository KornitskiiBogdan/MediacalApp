using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MedicalApp.Messages;
using MedicalApp.ViewModels.Interfaces;
using MedicalDatabase;
using MedicalDatabase.Objects;
using ReactiveUI;
using SkiaSharp;
using Tools.Messaging;

namespace MedicalApp.ViewModels.Documents
{
    public class DocumentViewModel : ViewModelBase, ISortedObject
    {
        private SKBitmap _bitmap;
        private readonly MedicalDocument _document;
        private readonly MedicalProject _medicalProject;

        public DocumentViewModel(MedicalProject project, MedicalDocument document)
        {
            _document = document;
            _medicalProject = project;
            _bitmap = new SKBitmap();
        }

        public override MedicalProject Project => _medicalProject;

        public DateTime? CurrentDateTime => _document.GetDateTime();

        public string Name
        {
            get => _document.Name;
            set
            {
                var backingField = _document.Name;
                this.RaiseAndSetIfChanged(ref backingField, value);
            } 
        }

        public SKBitmap Bitmap
        {
            get => _bitmap;
            set => _bitmap = value;
        }
    }
}
