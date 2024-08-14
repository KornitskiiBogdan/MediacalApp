using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using MedicalApp.ViewModels.Analysis;
using MedicalApp.ViewModels.Tabs;

namespace MedicalApp.Views.Tabs
{
    public partial class MarkViewTab : UserControl
    {
        public MarkViewTab()
        {
            InitializeComponent();
        }

        private void ButtonFlyout_OnOpened(object? sender, EventArgs e)
        {
            this.Effect = new BlurEffect(){Radius = 5};
        }

        private void ButtonFlyout_OnClosed(object? sender, EventArgs e)
        {
            this.Effect = new BlurEffect() { Radius = 0 };
        }

        private void OkButton_OnClick(object? sender, RoutedEventArgs e)
        {
            //TODO обработку ошибок при вводе
            if (DataContext is MarkViewModelTab tab)
            {
                if (float.TryParse((string?)ValueTextBox.Text, CultureInfo.InvariantCulture, out float fValue))
                {
                    tab.ViewModel.AddNewValue(DateTime.Parse(InputDateTextBox.Text ?? string.Empty), fValue);

                    InputDateTextBox.Clear();
                    ValueTextBox.Clear();
                }
            }
        }
    }
}
