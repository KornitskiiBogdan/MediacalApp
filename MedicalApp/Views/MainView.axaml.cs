using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using MedicalApp.Messages;
using MedicalApp.Models;
using MedicalApp.ViewModels;
using MedicalApp.ViewModels.Documents;
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
                AllowMultiple = true
            });
            
            foreach (var file in files)
            {
                await Task.Run(() =>
                {
                    DocumentViewModel.Create(viewModel.Project, file.Path.AbsolutePath);
                });
            }
        }
    }

    
}
