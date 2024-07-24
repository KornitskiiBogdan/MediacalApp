using System.Collections.Generic;
using MedicalApp.Models;
using MedicalApp.ViewModels;
using MedicalApp.ViewModels.Analysis;
using MedicalApp.ViewModels.Documents;
using MedicalApp.ViewModels.Tabs;

namespace MedicalApp.Tools
{
    public static class ListItemTemplates
    {
        public static List<ListItemTemplate> GetTemplates()
        {
            return new()
            {
                new ListItemTemplate(typeof(ProfileViewModel), "ProfileRegular.png", "Профиль"),
                new ListItemTemplate(typeof(AnalysisViewModelTab), "AnalysisRegular.png", "Анализы"),
                new ListItemTemplate(typeof(AddingViewModel), "AddRegular.png", "Добавить"),
                new ListItemTemplate(typeof(DocumentsViewModelTab), "DocumentsRegular.png", "Документы"),
                new ListItemTemplate(typeof(SettingsViewModel), "SettingsRegular.png", "Настройки"),
            };
        }
    }
}
