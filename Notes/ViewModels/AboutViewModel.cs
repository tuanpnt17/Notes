using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Notes.ViewModels
{
    internal class AboutViewModel
    {
        public string Title { get; } = AppInfo.Name;
        public string Version { get; } = AppInfo.VersionString;
        public string MoreInfoUrl { get; } = "https://aka.ms/maui";
        public string Message { get; } = "This app is written in XAML and C# with .NET MAUI.";

        public ICommand ShowMoreInfoCommand { get; }

        public AboutViewModel()
        {
            ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
        }

        private async Task ShowMoreInfo()
        {
            await Launcher.Default.OpenAsync(MoreInfoUrl);
        }
    }
}
