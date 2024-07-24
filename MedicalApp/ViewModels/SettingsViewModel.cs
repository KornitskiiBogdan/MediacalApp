using MedicalApp.Attributes;
using MedicalDatabase;

namespace MedicalApp.ViewModels
{
    [MedicalExtension]
    public class SettingsViewModel : ViewModelBase
    {
        private readonly MedicalProject _medicalProject;

        public SettingsViewModel(MedicalProject project)
        {
            _medicalProject = project;
        }

        public override MedicalProject Project => _medicalProject;
    }
}
