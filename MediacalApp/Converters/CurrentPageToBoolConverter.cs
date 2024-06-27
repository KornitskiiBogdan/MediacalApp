using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediacalApp.ViewModels;

namespace MediacalApp.Converters
{
    public class CurrentPageToBoolConverter
    {
        public static FuncValueConverter<ViewModelBase, bool> BoolConverter { get; } =
            new(viewModel => viewModel is not LoginViewModel);
    }
}
