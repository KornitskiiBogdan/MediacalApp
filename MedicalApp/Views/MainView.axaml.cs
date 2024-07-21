using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using MedicalApp.Models;
using MedicalApp.ViewModels;

namespace MedicalApp.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private async void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        foreach (var item in e.AddedItems)
        {
            if (item is not ListItemTemplate itemTemplate ||
                itemTemplate.ModelType != typeof(AddingViewModel))
            {
                continue;
            }

            var topLevel = TopLevel.GetTopLevel(this);

            if (topLevel is null)
            {
                return;
            }

            // Start async operation to open the dialog.
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Выберите документ с анализами",
                FileTypeFilter = new [] {FilePickerFileTypes.Pdf}, 
                AllowMultiple = false
            });

            if (files.Count >= 1)
            {
                // Open reading stream from the first file.
                await using var stream = await files[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream);
                // Reads all the content of file as a text.
                var fileContent = await streamReader.ReadToEndAsync();
            }

            return;
        }
    }
}
