namespace Notes;

public partial class NotePage : ContentPage
{
    public string _fileName { get; } = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

    public NotePage()
    {
        InitializeComponent();
        if (File.Exists(_fileName))
            TextEditor.Text = File.ReadAllText(_fileName);
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        await File.WriteAllTextAsync(_fileName, TextEditor.Text);
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (File.Exists(_fileName))
            File.Delete(_fileName);

        TextEditor.Text = string.Empty;
    }
}
