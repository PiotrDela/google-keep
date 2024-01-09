using GoogleKeep.Api.Notes;
using GoogleKeep.Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace GoogleKeep.Tests.Notes
{
    public class NotesControllerTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldReturnBadRequestWhenTitleIsEmptyOrWhitespace(string title)
        {
            // given
            var httpClient = HttpClientFactory.Create();

            // when
            var response = await httpClient.PostAsJsonAsync("/api/notes", new CreateNoteRequest() { Title = title });

            // then
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnCreatedStatusCode()
        {
            // given
            const string noteTitle = "Lorem ipsum";
            var httpClient = HttpClientFactory.Create();

            // when
            var response = await httpClient.PostAsJsonAsync("/api/notes", new CreateNoteRequest() { Title = noteTitle });
            var noteId = await response.Content.ReadFromJsonAsync<NoteId>();

            // then
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(noteId);
        }
    }
}
