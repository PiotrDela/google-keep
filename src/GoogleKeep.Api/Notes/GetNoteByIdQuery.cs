using GoogleKeep.Api.Notes.ApiModel;
using GoogleKeep.Api.Queries;

namespace GoogleKeep.Api.Notes
{
    public class GetNoteByIdQuery: IQuery<NoteDto>
    {
        public Guid NoteId { get; }

        public Guid RequestingUserId { get; }

        public GetNoteByIdQuery(Guid noteId, Guid requestingUserId)
        {
            this.NoteId = noteId;
            this.RequestingUserId = requestingUserId;
        }
    }
}
