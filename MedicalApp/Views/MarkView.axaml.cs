using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;

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
    }
}
