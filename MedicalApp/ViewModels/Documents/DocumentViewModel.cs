using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalApp.ViewModels.Interfaces;
using SkiaSharp;

namespace MedicalApp.ViewModels.Documents
{
    public class DocumentViewModel : ViewModelBase, ISortedObject
    {
        
        private string _name;
        private SKBitmap _bitmap;
        private readonly DateTime? _currentDateTime;

        public DocumentViewModel(SKBitmap bitmap, string name)
        {
            _bitmap = bitmap;
            _name = name;
        }

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
