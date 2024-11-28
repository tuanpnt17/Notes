namespace Notes.Models
{
    public class Note
    {
        public string? FileName { get; set; } = $"{Path.GetRandomFileName()}.notes.txt";
        public string? Text { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;

        public void Save() =>
            File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, FileName), Text);

        public void Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, FileName));

        public static Note Load(string? fileName)
        {
            fileName = Path.Combine(FileSystem.AppDataDirectory, fileName);
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Unable to file find on local storage", fileName);
            }

            return new Note
            {
                FileName = fileName,
                Text = File.ReadAllText(fileName),
                Date = File.GetCreationTime(fileName),
            };
        }

        public static IEnumerable<Note> LoadAll()
        {
            // Get the folder where all note are store
            var appDataPath = FileSystem.AppDataDirectory;

            // Use LINQ to load *.notes.txt files
            return Directory
                .EnumerateFiles(appDataPath, "*.notes.txt")
                .Select(Load)
                .OrderByDescending(note => note.Date);
        }
    }
}
