using GoogleKeep.Api.Commands;
using GoogleKeep.Domain.Entities;

namespace GoogleKeep.Api.Notes
{
    public class CreateNoteCommandCommand: ICommand<NoteId>
    {
        public string Title { get; }

        public CreateNoteCommandCommand(string title)
        {
            this.Title = title;
        }
    }
}
