using MedicalApp.Attributes;
using MedicalDatabase;

namespace MedicalApp.ViewModels
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
