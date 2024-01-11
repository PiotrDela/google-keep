using GoogleKeep.Api.Commands;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Domain.Users;

namespace GoogleKeep.Api.Notes
{
    public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommandCommand, NoteId>
    {
        private readonly INoteRepository repository;

        public CreateNoteCommandHandler(INoteRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<NoteId> Handle(CreateNoteCommandCommand request, CancellationToken cancellationToken)
        {
            var noteOwner = new User(new UserId(request.OwnerId));
            var note = Note.Create(request.Title, noteOwner);
            await repository.AddAsync(note);
            return note.Id;
        }
    }
}
