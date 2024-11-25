namespace Notes.Models
{
    public class About
    {
        public string Title { get; set; } = AppInfo.Name;
        public string Version { get; set; } = AppInfo.VersionString;
        public string MoreInfoUrl { get; set; } = "https://aka.ms/maui";
        public string Message { get; set; } =
            "This app is written in XAML and C# with .NET MAUI with Tuan Pham";
    }
}
