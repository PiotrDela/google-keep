using GoogleKeep.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace GoogleKeep.Domain.Entities
{
    public class Note: Entity, IAggregateRoot
    {
        public NoteId Id { get; }
        public string Title { get; }
        public NoteContent Content { get; set; }

        public static Note Create(string title)
        {
            return new Note(new NoteId(Guid.NewGuid()), title, new NoteContent());
        }

        [JsonConstructor]
        private Note(NoteId id, string title, NoteContent content)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"{nameof(title)} cannot be null nor whitespace", nameof(title));
            }

            Id = id;
            Title = title;
            Content = content;
        }
    }
}
