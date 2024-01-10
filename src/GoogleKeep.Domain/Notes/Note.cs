using GoogleKeep.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace GoogleKeep.Domain.Entities
{
    public class Note: Entity, IAggregateRoot
    {
        public NoteId Id { get; }
        public string Title { get; }
        public NoteContent Content { get; }
        public User Owner { get; }

        public static Note Create(string title, User owner)
        {
            return new Note(new NoteId(Guid.NewGuid()), title, new NoteContent(), owner);
        }

        [JsonConstructor]
        private Note(NoteId id, string title, NoteContent content, User owner)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"{nameof(title)} cannot be null nor whitespace", nameof(title));
            }

            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.Owner = owner;
        }
    }
}
