using MedicalApp.Attributes;
using MedicalDatabase;
using ReactiveUI;

namespace MedicalApp.ViewModels
{
    [MedicalExtension]
    public class ProfileViewModel : ViewModelBase
    {
        private readonly MedicalProject _medicalProject;
        private int _age;
        private int _growth;
        private float _weight;

        public ProfileViewModel(MedicalProject project)
        {
            _medicalProject = project;
        }

        public int Age
        {
            get => _age;
            set => this.RaiseAndSetIfChanged(ref _age, value);
        }

        public int Growth
        {
            get => _growth;
            set => this.RaiseAndSetIfChanged(ref _growth, value);
        }

        public float Weight
        {
            get => _weight;
            set => this.RaiseAndSetIfChanged(ref _weight, value);
        }

        public override MedicalProject Project => _medicalProject;
    }
}
