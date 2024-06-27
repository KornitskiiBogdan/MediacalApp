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
                new ListItemTemplate(typeof(AnalysisViewModel), "AnalysisRegular", "Analysis"),
                new ListItemTemplate(typeof(DocumentsViewModel), "DocumentsRegular", "Documents"),
                new ListItemTemplate(typeof(ProfileViewModel), "ProfileRegular", "Profile"),
                new ListItemTemplate(typeof(SettingsViewModel), "SettingsRegular", "Settings"),
            };
        }
    }
    
}
