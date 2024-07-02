﻿using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using MedicalApp.ViewModels;

namespace MedicalApp
{
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? param)
        {
            if (param is null)
            {
                return null;
            }

            var name = param.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                var control = (Control?)Activator.CreateInstance(type);
                if (control != null)
                {
                    control.DataContext = param;
                }

                return control;
            }

            throw new NotImplementedException();
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
