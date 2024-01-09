using GoogleKeep.Domain.Entities;

namespace GoogleKeep.Infrastructure.Notes
{
    public class NoteInMemoryRepository : INoteRepository
    {
        private readonly IDictionary<NoteId, Note> notes = new Dictionary<NoteId, Note>();

        public Task AddAsync(Note entity)
        {
            notes.Add(entity.Id, entity);
            return Task.CompletedTask;            
        }
    }
}
