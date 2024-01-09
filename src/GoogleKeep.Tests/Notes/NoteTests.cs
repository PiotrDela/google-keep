using GoogleKeep.Domain.Entities;

namespace GoogleKeep.Tests.Notes
{
    public class NoteTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldNotAllowToCreateNoteWithTitleEmptyOrWhitespace(string title)
        {
            Assert.Throws<ArgumentException>(() => Note.Create(title));
        }

        [Fact]
        public void CreateShouldAssignValues()
        {
            // given
            const string title = "New note";

            // when
            var note = Note.Create(title);

            // then
            Assert.Equal(title, note.Title);
            Assert.NotNull(note.Content);
            Assert.NotNull(note.Id);
        }
    }
}
