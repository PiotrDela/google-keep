using GoogleKeep.Domain.Entities;
using System.Text.Json.Serialization;

namespace GoogleKeep.Api.Notes.ApiModel
{
    public class NoteDto
    {
        public static NoteDto Create(Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            return new NoteDto(note.Id.Value, note.Title, note.Content);
        }

        [JsonConstructor]
        private NoteDto(Guid id, string title, object content)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
        }

        public Guid Id { get; }
        public string Title { get; }
        public object Content { get; }
    }
}
