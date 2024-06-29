using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediacalApp.Models;
using MediacalApp.ViewModels;

namespace MediacalApp.Tools
{
    public static class ListItemTemplates
    {
        public static List<ListItemTemplate> GetTemplates()
        {
            return new()
            {
                new ListItemTemplate(typeof(ProfileViewModel), "ProfileRegular.png", "Профиль"),
                new ListItemTemplate(typeof(AnalysisViewModel), "AnalysisRegular.png", "Анализы"),
                new ListItemTemplate(typeof(AddingViewModel), "AddRegular.png", "Добавить"),
                new ListItemTemplate(typeof(DocumentsViewModel), "DocumentsRegular.png", "Документы"),
                new ListItemTemplate(typeof(SettingsViewModel), "SettingsRegular.png", "Настройки"),
            };
        }
    }
    
}
