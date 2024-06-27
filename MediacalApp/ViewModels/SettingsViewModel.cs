using MediacalApp.Attributes;
using MediacalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediacalApp.ViewModels
{
    [MedicalExtension]
    public class SettingsViewModel : ViewModelBase
    {
        private readonly MedicalProject _medicalProject;

        public SettingsViewModel(MedicalProject project)
        {
            _medicalProject = project;
        }
    }
}
