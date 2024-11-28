using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Notes.Models;
using Notes.Views;

namespace Notes.ViewModels
{
    internal class NotesViewModel : IQueryAttributable
    {
        public ObservableCollection<NoteViewModel> AllNotes { get; set; } // list all notes
        public ICommand NewCommand { get; } // New note command
        public ICommand SelectNoteCommand { get; } // select a note command

        public NotesViewModel()
        {
            AllNotes = new ObservableCollection<NoteViewModel>(
                Note.LoadAll().Select(n => new NoteViewModel(n))
            );
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<NoteViewModel>(SelectNoteAsync);
        }

        private static async Task SelectNoteAsync(NoteViewModel? note)
        {
            if (note != null)
            {
                await Shell.Current.GoToAsync($"{nameof(NotePage)}?load={note.Identifier}");
            }
        }

        private static async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(NotePage));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("deleted", out var deletedQuery))
            {
                var noteId = deletedQuery.ToString();
                var matchedNote = AllNotes.FirstOrDefault(n => n.Identifier == noteId);
                if (matchedNote != null)
                {
                    AllNotes.Remove(matchedNote);
                }
            }
            else if (query.TryGetValue("saved", out var updatedQuery))
            {
                var noteId = updatedQuery.ToString();
                var matchedNote = AllNotes.FirstOrDefault(n => n.Identifier == noteId);
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                }
                else
                {
                    if (noteId != null)
                        AllNotes.Insert(0, new NoteViewModel(Note.Load(noteId)));
                }
            }
        }
    }
}
