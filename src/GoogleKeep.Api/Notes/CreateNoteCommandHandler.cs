using GoogleKeep.Api.Commands;
using GoogleKeep.Domain.Entities;

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
            var note = Note.Create(request.Title);

            await repository.AddAsync(note);

            return note.Id;
        }
    }
}
