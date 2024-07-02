using Avalonia.Data.Converters;
using MedicalApp.ViewModels;

namespace MedicalApp.Converters
{
    public class CurrentPageToBoolConverter
    {
        public static FuncValueConverter<ViewModelBase, bool> BoolConverter { get; } =
            new(viewModel => viewModel is not LoginViewModel);
    }
}
