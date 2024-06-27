using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediacalApp.Attributes;
using MediacalApp.Models;

namespace MediacalApp.ViewModels
{
    [MedicalExtension]
    public class DocumentsViewModel : ViewModelBase
    {
        private readonly MedicalProject _medicalProject;

        public DocumentsViewModel(MedicalProject project)
        {
            _medicalProject = project;
        }
    }
}
