using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using MedicalApp.Messages;
using MedicalDatabase;
using MedicalDatabase.Objects;
using MedicalDatabase.Operations;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Tools.Messaging;

namespace MedicalApp.ViewModels
{
    public class MarkViewModel : ViewModelBase
    {
        private DateTime? _currentDatetime;
        private string? _currentValue;

        private readonly List<MedicalValue> _values;
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
            _values = new List<MedicalValue>(_repository.Reader.ReadValues(_mark));
            _referenceModel = GetCurrentReference();
            _currentDatetime = _values.LastOrDefault()?.GetDateTime();
            _currentValue = _values.LastOrDefault()?.Value.ToString(CultureInfo.CurrentCulture);
            CalculateReferences();
        }

        public string Name
        {
            get => _mark.Name;
            set
            {
                var backingField = _mark.Name;
                this.RaiseAndSetIfChanged(ref backingField, value);
            }
        }

        public DateTime? CurrentDatetime
        {
            get => _currentDatetime;
            private set => this.RaiseAndSetIfChanged(ref _currentDatetime, value);
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

        private MedicalReference GetCurrentReference()
        {
            //TODO
            //На основании профиля пользователя мы должны вытащить для него нужный референс

            return _repository.Reader.ReadReferences(_mark).LastOrDefault() ?? new MedicalReference(0, _mark.Id, "", "");
        }

        public void AddNewValue(DateTime date, float value)
        {
            CurrentValue = value.ToString(CultureInfo.CurrentCulture);
            CurrentDatetime = date;
            var markValue = new MedicalValue(0, _mark.Id, value, date.Ticks);
            _repository.Writer.Write(new [] { markValue });
            _values.Add(markValue);
        }

        public void CalculateReferences()
        {
            if(_referenceModel.LowerValue == null || _referenceModel.UpperValue == null)
            {
                return;
            }

            if (_values.Count == 0)
            {
                return;
            }

            var medicalValue = _values.Last();

            
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
                TopPosition = (int)((_referenceModel.UpperValue.Value / medicalValue.Value) * 7) - 2; 
                return;
            }

            if (medicalValue.Value <= _referenceModel.LowerValue.Value)
            {
                TopPosition = (int)((medicalValue.Value / _referenceModel.LowerValue.Value) * 7) + 25;
                return;
            }

            var koefA = (_referenceModel.UpperValue.Value - _referenceModel.LowerValue.Value) / 16;
            var koefB = _referenceModel.LowerValue.Value;
            TopPosition = (int)((medicalValue.Value - koefB) / koefA) + 5;
        }

        public async Task GoBackCommand()
        {
            await _project.MessageBus.SendAsync(new GoBackView(GetType()));
        }
    }
}
