using GoogleKeep.Domain.SeedWork;

namespace GoogleKeep.Domain.Entities
{
    public class Note: Entity, IAggregateRoot
    {
        public NoteId Id { get; }
        public string Title { get; }
        public NoteContent Content { get; set; }

        public static Note Create(string title)
        {
            return new Note(title, new NoteContent());
        }

        private Note(string title, NoteContent content)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"{nameof(title)} cannot be null nor whitespace", nameof(title));
            }

            Title = title;
            Content = content;

            Id = new NoteId(Guid.NewGuid());
        }
    }
}
