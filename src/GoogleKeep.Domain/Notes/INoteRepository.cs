using GoogleKeep.Domain.SeedWork;

namespace GoogleKeep.Domain.Entities
{
    public interface INoteRepository: IRepository<Note> 
    {
        Task<Note> GetAsync(NoteId noteId);
    }
}
