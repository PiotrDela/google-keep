using GoogleKeep.Domain.SeedWork;

namespace GoogleKeep.Domain.Entities
{
    public class Note: Entity, IAggregateRoot
    {
        public NoteId Id { get; }
        public string Title { get; }

        public static Note Create(string title)
        {
            return new Note(title);
        }

        private Note(string title)
        {
            Id = new NoteId(Guid.NewGuid());
            Title = title;
        }
    }
}
