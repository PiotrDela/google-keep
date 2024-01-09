using GoogleKeep.Api.Notes.ApiModel;
using GoogleKeep.Api.Queries;
using GoogleKeep.Domain.Entities;

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

            return new NoteDto
            {
                Id = note.Id.Value,
                Title = note.Title,
            };            
        }
    }
}
