using GoogleKeep.Api.Commands;
using GoogleKeep.Domain.Entities;

namespace GoogleKeep.Api.Notes.CreateNote
{
    public class CreateNoteCommandCommand : ICommand<NoteId>
    {
        public string Title { get; }

        public Guid OwnerId { get; }

        public CreateNoteCommandCommand(string title, Guid ownerId)
        {
            Title = title;
            OwnerId = ownerId;
        }
    }
}
