using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using MediacalApp.Messaging;
using MediacalApp.Messaging.Messages;
using MediacalApp.Models;
using ReactiveUI;

namespace MediacalApp.ViewModels
{
    public class MarkViewModel : ViewModelBase
    {
        private Unit _unit;
        private string _name;
        private DateTime _currentDatetime;
        private string _currentValue;

        private readonly List<float> _values;
        private SolidColorBrush? _colorText;
        private int _topPosition;
        private readonly MarkModel _model;
        private readonly MedicalProject _project;

        public MarkViewModel(MarkModel model, MedicalProject project,
            string name, DateTime date, float value, string unit)
        {
            _model = model;
            _project = project;
            _name = name;
            _currentDatetime = date;
            _currentValue = value.ToString(CultureInfo.CurrentCulture);
            _unit = new Unit(unit);
            _values = new List<float>{value};
            CalculateReferences();
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public DateTime CurrentDatetime
        {
            get => _currentDatetime;
            set => this.RaiseAndSetIfChanged(ref _currentDatetime, value);
        }

        public string CurrentValue
        {
            get => _currentValue;
            set => this.RaiseAndSetIfChanged(ref _currentValue, value);
        }

        public Unit Unit
        {
            get => _unit;
            set => this.RaiseAndSetIfChanged(ref _unit, value);
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

        public void AddNewValue(DateTime date, float value)
        {
            CurrentValue = value.ToString(CultureInfo.CurrentCulture);
            CurrentDatetime = date;
            _values.Add(value);
        }

        public void CalculateReferences()
        {
            if(_model.LowerValue == null || _model.UpperValue == null)
            {
                return;
            }

            var value = _values.Last();

            
            if (value > _model.LowerValue.Value && value < _model.UpperValue.Value)
            {
                ColorText = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ColorText = new SolidColorBrush(Colors.Red);
            }

            if (value >= _model.UpperValue.Value)
            {
                TopPosition = (int)((_model.UpperValue.Value / value) * 7) - 2; 
                return;
            }

            if (value <= _model.LowerValue.Value)
            {
                TopPosition = (int)((value / _model.LowerValue.Value) * 7) + 25;
                return;
            }

            var koefA = (_model.UpperValue.Value - _model.LowerValue.Value) / 16;
            var koefB = _model.LowerValue.Value;
            TopPosition = (int)((value - koefB) / koefA) + 5;
        }

        public async Task GoBackCommand()
        {
            await _project.MessageBus.SendAsync(new GoBackView(GetType()));
        }

        public async Task GoNextCommand()
        {
            await _project.MessageBus.SendAsync(new GoNextView(GetType()));
        }
    }
}
