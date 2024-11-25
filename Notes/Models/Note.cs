namespace Notes.Models
{
    public class Note
    {
        public required string FileName { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
    }
}
