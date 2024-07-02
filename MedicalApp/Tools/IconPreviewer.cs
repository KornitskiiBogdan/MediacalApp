using System;
using Avalonia;
using Avalonia.Controls;

namespace MedicalApp.Tools
{
    public sealed class IconPreviewer : UserControl
    {
        public static readonly StyledProperty<object?> KeyProperty = AvaloniaProperty.Register<IconPreviewer, object?>("Key");

        public object? Key
        {
            get => GetValue(KeyProperty);
            set => SetValue(KeyProperty, value);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if (Name is null)
            {
                throw new InvalidOperationException("Name must be set before control is attached to visual tree");
            }

            var data = this.FindResource(Name);
            SetCurrentValue(KeyProperty, data);
            base.OnAttachedToVisualTree(e);
        }
    }
}
