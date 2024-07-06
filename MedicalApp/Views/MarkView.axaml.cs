using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using MedicalApp.ViewModels;

namespace MedicalApp.Views
{
    public partial class MarkView : UserControl
    {
        public MarkView()
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
            if (DataContext is MarkViewModel viewModel)
            {
                if (float.TryParse(ValueTextBox.Text, out float fValue))
                {
                    viewModel.AddNewValue(DateTime.Parse(InputDateTextBox.Text ?? string.Empty), fValue);

                    InputDateTextBox.Clear();
                    ValueTextBox.Clear();
                }
            }
        }
    }
}
