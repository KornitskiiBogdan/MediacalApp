using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using LiveChartsWrapper;
using MedicalApp.Messages;
using MedicalApp.Tools;
using MedicalApp.ViewModels.Interfaces;
using MedicalDatabase;
using MedicalDatabase.Objects;
using MedicalDatabase.Operations;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Tools.Messaging;

namespace MedicalApp.ViewModels.Analysis
{
    public class MarkViewModel : ViewModelBase, ISortedObject
    {
        private DateTime? _currentDateTime;
        private string? _currentValue;

        private SolidColorBrush? _colorText;
        private int _topPosition;
        private MedicalReference _referenceModel;
        private readonly MedicalProject _project;
        private readonly MedicalMark _mark;
        private readonly MedicalRepository _repository;

        public MarkViewModel(MedicalMark markModel, MedicalProject project)
        {
            _mark = markModel;
            _project = project;
            _repository = _project.Services.GetRequiredService<MedicalRepository>();
            Values = new ObservableCollection<MedicalValue>(_repository.Reader.ReadValues(_mark));
            _referenceModel = GetCurrentReference();
            _currentDateTime = Values.LastOrDefault()?.GetDateTime();
            _currentValue = Values.LastOrDefault()?.Value.ToString(CultureInfo.CurrentCulture);
            Graphic = new MedicalMarksGraphics(CurrentReference, Values);
            CalculateReferences();
            Units.Add(markModel.Unit);
        }

        public override MedicalProject Project => _project;

        public string Name
        {
            get => _mark.Name;
            set
            {
                var backingField = _mark.Name;
                this.RaiseAndSetIfChanged(ref backingField, value);
            }
        }

        public DateTime? CurrentDateTime
        {
            get => _currentDateTime;
            private set => this.RaiseAndSetIfChanged(ref _currentDateTime, value);
        }

        public string? CurrentValue
        {
            get => _currentValue;
            private set => this.RaiseAndSetIfChanged(ref _currentValue, value);
        }

        public Unit Unit
        {
            get => _mark.Unit;
            set
            {
                var backingField = _mark.Unit;
                this.RaiseAndSetIfChanged(ref backingField, value);
            }
        }

        public ObservableCollection<Unit> Units { get; set; } = new ObservableCollection<Unit>();

        public SolidColorBrush? ColorText
        {
            get => _colorText;
            set => this.RaiseAndSetIfChanged(ref _colorText, value);
        }

        public int TopPosition
        {
            get => _topPosition;
            set => this.RaiseAndSetIfChanged(ref _topPosition, value);
        }

        public MedicalReference CurrentReference
        {
            get => _referenceModel;
            set => this.RaiseAndSetIfChanged(ref _referenceModel, value);
        }

        public MedicalMarksGraphics Graphic { get; set; }

        public ObservableCollection<MedicalValue> Values { get; }

        private MedicalReference GetCurrentReference()
        {
            //TODO
            //На основании профиля пользователя мы должны вытащить для него нужный референс

            return _repository.Reader.ReadReferences(_mark).LastOrDefault() ?? new MedicalReference(0, _mark.Id, "", "");
        }

        public void AddNewValue(DateTime date, float value)
        {
            CurrentValue = value.ToString(CultureInfo.CurrentCulture);
            CurrentDateTime = date;
            var markValue = new MedicalValue(0, _mark.Id, value, date.Ticks);
            _repository.Writer.Write(new[] { markValue });
            Values.Add(markValue);
            CalculateReferences();
        }

        public void CalculateReferences()
        {
            if (_referenceModel.LowerValue == null || _referenceModel.UpperValue == null)
            {
                return;
            }

            if (Values.Count == 0)
            {
                return;
            }

            var medicalValue = Values.Last();


            if (medicalValue.Value > _referenceModel.LowerValue.Value && medicalValue.Value < _referenceModel.UpperValue.Value)
            {
                ColorText = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ColorText = new SolidColorBrush(Colors.Red);
            }

            if (medicalValue.Value >= _referenceModel.UpperValue.Value)
            {
                TopPosition = (int)(_referenceModel.UpperValue.Value / medicalValue.Value * 7) - 2;
                return;
            }

            if (medicalValue.Value <= _referenceModel.LowerValue.Value)
            {
                TopPosition = (int)(medicalValue.Value / _referenceModel.LowerValue.Value * 7) + 25;
                return;
            }

            var koefA = (_referenceModel.UpperValue.Value - _referenceModel.LowerValue.Value) / 16;
            var koefB = _referenceModel.LowerValue.Value;
            TopPosition = (int)((medicalValue.Value - koefB) / koefA) + 5;
        }
    }
}
