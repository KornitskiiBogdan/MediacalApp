using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MedicalDatabase;
using MedicalDatabase.Objects;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace MedicalApp.ViewModels
{
    public class AddingViewModel : ViewModelBase
    {
        private readonly MedicalProject _project;
        private readonly ObservableCollection<MedicalMark> _nameMarks;
        private MedicalMark? _selectedMark;
        private string? _nameGroup;
        private string? _nameSubGroup;
        private string? _inputValues;

        public AddingViewModel(MedicalProject project, MedicalMark[] marks)
        {
            _project = project;
            _nameMarks = new ObservableCollection<MedicalMark>(marks);
        }

        public ObservableCollection<MedicalMark> NameMarks => _nameMarks;

        public string? NameGroup
        {
            get => _nameGroup;
            private set => this.RaiseAndSetIfChanged(ref _nameGroup, value);
        }

        public string? NameSubGroup
        {
            get => _nameSubGroup;
            private set => this.RaiseAndSetIfChanged(ref _nameSubGroup, value);
        }

        public string InputValues
        {
            get => _inputValues;
            set => this.RaiseAndSetIfChanged(ref _inputValues, value);
        }

        public MedicalMark? SelectedMark
        {
            get => _selectedMark;
            set
            {
                if (value == null)
                {
                    return;
                }
                this.RaiseAndSetIfChanged(ref _selectedMark, value);
                NameGroup = value.NameGroup;
                NameSubGroup = value.NameSubGroup;
            }
        }

        public void OkCommand()
        {
            if (string.IsNullOrEmpty(InputValues))
            {
                return;
            }

            if (SelectedMark == null)
            {
                return;
            }

            var values = InputValues.Split(" ");
            var medicalValues = new List<MedicalValue>();

            foreach (var v in values)
            {
                if (float.TryParse(v, out float fV))
                {
                    medicalValues.Add(new MedicalValue(0, SelectedMark.Id, fV, DateTime.Now.Ticks));
                }
            }

            var rep = _project.Services.GetRequiredService<MedicalRepository>();
            rep.Writer.Write(medicalValues.ToArray());
        }
    }
}
