using GoogleKeep.Api.Notes.ApiModel;
using GoogleKeep.Api.Queries;

namespace GoogleKeep.Api.Notes
{
    public class GetUserNotesQuery : IQuery<IEnumerable<NoteDto>>
    {
        public Guid UserId { get; }

        public GetUserNotesQuery(Guid userId)
        {
            this.UserId = userId;
        }
    }
}
