using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using MedicalApp.Models;
using MedicalApp.ViewModels;
using MedicalDatabase;
using MedicalDatabase.Objects;
using MedicalDatabase.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalApp.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private async void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is not MainViewModel viewModel)
        {
            return;
        }

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
            
            foreach (var file in files)
            {
                await Task.Run(() =>
                {
                    var bitmap = PDFReader.PdfReader.GetBitmapFromPdf(file.Path.AbsolutePath);

                    var writeToDatabase = viewModel.Project.Services.GetRequiredService<MedicalRepository>();

                    writeToDatabase.Writer.Write(new MedicalDocument[]
                    {
                        new(id: 0, name: file.Name, width: bitmap.Width, height: bitmap.Height, image: bitmap.Bytes)
                    });
                });
            }
        }
    }
}
