using MediacalApp.Attributes;
using MediacalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalDatabase;

namespace MediacalApp.ViewModels
{
    [MedicalExtension]
    public class ProfileViewModel : ViewModelBase
    {
        private readonly MedicalProject _medicalProject;

        public ProfileViewModel(MedicalProject project)
        {
            _medicalProject = project;
        }
    }
}
