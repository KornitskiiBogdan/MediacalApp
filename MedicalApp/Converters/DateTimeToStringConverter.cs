using Avalonia.Data.Converters;
using MedicalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Converters
{
    public class DateTimeToStringConverter
    {
        public static FuncValueConverter<DateTime, string> StringConverter { get; } =
            new(dateTime => dateTime.ToString("dd.MM.yy"));
    }
}
