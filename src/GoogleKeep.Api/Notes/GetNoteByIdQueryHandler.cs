using GoogleKeep.Api.Notes.ApiModel;
using GoogleKeep.Api.Queries;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Domain.Users;

namespace GoogleKeep.Api.Notes
{
    public class GetNoteByIdQueryHandler : IQueryHandler<GetNoteByIdQuery, NoteDto>
    {
        private readonly INoteRepository repository;

        public GetNoteByIdQueryHandler(INoteRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<NoteDto> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var note = await repository.GetAsync(new NoteId(request.NoteId));
            if (note == null)
            {
                return null;
            }

            var requestingUserId = new UserId(request.RequestingUserId);
            if (note.Owner.Id != requestingUserId)
            {
                throw new UnauthorizedAccessException();
            }

            return NoteDto.Create(note);
        }
    }
}
