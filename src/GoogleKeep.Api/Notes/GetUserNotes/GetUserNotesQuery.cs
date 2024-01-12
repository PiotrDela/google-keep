using GoogleKeep.Api.Notes.ApiModel;
using GoogleKeep.Api.Queries;

namespace GoogleKeep.Api.Notes.GetUserNotes
{
    public class GetUserNotesQuery : IQuery<IEnumerable<NoteDto>>
    {
        public Guid UserId { get; }

        public GetUserNotesQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
