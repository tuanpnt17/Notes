using Notes.Models;

namespace Notes.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    public string ItemId
    {
        set { LoadNote(value); }
    }

    public NotePage()
    {
        InitializeComponent();

        var appDataPath = FileSystem.AppDataDirectory;
        var randomFileName = $"{Path.GetRandomFileName()}.notes.txt";
        LoadNote(Path.Combine(appDataPath, randomFileName));
    }

    private async void LoadNote(string fileName)
    {
        var noteModel = new Note() { FileName = fileName };
        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = await File.ReadAllTextAsync(fileName);
        }
        BindingContext = noteModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Note note)
        {
            await File.WriteAllTextAsync(note.FileName, TextEditor.Text);
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Note note)
        {
            if (File.Exists(note.FileName))
                File.Delete(note.FileName);
            TextEditor.Text = string.Empty;
        }
        await Shell.Current.GoToAsync("..");
    }
}
