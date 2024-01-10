using GoogleKeep.Api.Commands;
using GoogleKeep.Domain.Entities;

namespace GoogleKeep.Api.Notes
{
    public class CreateNoteCommandCommand: ICommand<NoteId>
    {
        public string Title { get; }

        public Guid OwnerId { get; }

        public CreateNoteCommandCommand(string title, Guid ownerId)
        {
            this.Title = title;
            this.OwnerId = ownerId;
        }
    }
}
