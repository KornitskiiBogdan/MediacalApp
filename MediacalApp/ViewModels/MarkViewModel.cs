using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using Avalonia.Media;
using MediacalApp.Models;
using MediacalApp.ViewModels.Charts;
using ReactiveUI;

namespace MediacalApp.ViewModels
{
    public class MarkViewModel : ViewModelBase
    {
        private Unit _unit;
        private string _name;
        private string _currentDatetime;
        private string _currentValue;

        private readonly List<float> _values;
        private SolidColorBrush? _colorText;
        private int _topPosition;
        private float? _upperValue;
        private float? _lowerValue;

        public MarkViewModel(string name, string date, float value, string unit, 
            float? upperValue, float? lowerValue)
        {
            _name = name;
            _currentDatetime = date;
            _currentValue = value.ToString(CultureInfo.CurrentCulture);
            _unit = new Unit(unit);
            _values = new List<float>{value};
            _upperValue = upperValue;
            _lowerValue = lowerValue;
            CalculateReferences();
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string CurrentDatetime
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

        public ObservableCollection<Unit> Units { get; set; }

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

        public void AddNewValue(string date, float value)
        {
            CurrentValue = value.ToString(CultureInfo.CurrentCulture);
            CurrentDatetime = date;
            _values.Add(value);
        }

        public void CalculateReferences()
        {
            if(_lowerValue == null ||  _upperValue == null)
            {
                return;
            }

            var value = _values.Last();

            
            if (value > _lowerValue.Value && value < _upperValue.Value)
            {
                ColorText = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ColorText = new SolidColorBrush(Colors.Red);
            }

            if (value >= _upperValue.Value)
            {
                TopPosition = (int)((_upperValue.Value / value) * 7) - 2; 
                return;
            }

            if (value <= _lowerValue.Value)
            {
                TopPosition = (int)((value / _lowerValue.Value) * 7) + 25;
                return;
            }

            var koefA = (_upperValue.Value - _lowerValue.Value) / 16;
            var koefB = _lowerValue.Value;
            TopPosition = (int)((value - koefB) / koefA) + 5;
        }
    }
}
