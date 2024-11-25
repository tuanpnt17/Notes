using System.Collections.ObjectModel;

namespace Notes.Models
{
    public class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new();

        public AllNotes()
        {
            LoadNotes();
        }

        public void LoadNotes()
        {
            Notes.Clear();

            //Get the folder where the notes are stored
            var appDataPath = FileSystem.AppDataDirectory;

            // Using LINQ to load the *.notes.txt file
            var notes = Directory
                .EnumerateFiles(appDataPath, "*.notes.txt")
                .Select(fileName => new Note()
                {
                    FileName = fileName,
                    Date = File.GetCreationTime(fileName),
                    Text = File.ReadAllText(fileName),
                });

            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }
    }
}
