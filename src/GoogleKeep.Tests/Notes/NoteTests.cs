using GoogleKeep.Domain.Entities;
using GoogleKeep.Domain.Users;
using Xunit;

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
            var owner = new User(new UserId(Guid.NewGuid()));
            Assert.Throws<ArgumentException>(() => Note.Create(title, owner));
        }

        [Fact]
        public void CreateShouldAssignValues()
        {
            // given
            const string title = "New note";
            var owner = new User(new UserId(Guid.NewGuid()));

            // when
            var note = Note.Create(title, owner);

            // then
            Assert.Equal(title, note.Title);
            Assert.NotNull(note.Content);
            Assert.NotNull(note.Id);
            Assert.Equal(owner, note.Owner);
        }
    }
}
