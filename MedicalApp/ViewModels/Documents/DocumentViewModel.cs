using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalApp.Messages;
using MedicalApp.ViewModels.Interfaces;
using MedicalDatabase;
using SkiaSharp;
using Tools.Messaging;

namespace MedicalApp.ViewModels.Documents
{
    public class DocumentViewModel : ViewModelBase, ISortedObject
    {
        
        private string _name;
        private SKBitmap _bitmap;
        private readonly DateTime _currentDateTime;
        private readonly MedicalProject _medicalProject;

        public DocumentViewModel(MedicalProject project, SKBitmap bitmap, string name)
        {
            _name = name;
            _bitmap = bitmap;
            _medicalProject = project;
            _currentDateTime = DateTime.Today;
        }

        public override MedicalProject Project => _medicalProject;

        public DateTime? CurrentDateTime => _currentDateTime;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public SKBitmap Bitmap
        {
            get => _bitmap;
            set => _bitmap = value;
        }
    }
}
