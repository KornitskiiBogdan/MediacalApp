using MedicalApp.Attributes;
using MedicalDatabase;

namespace MedicalApp.ViewModels
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
