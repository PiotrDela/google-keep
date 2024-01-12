using GoogleKeep.Api.Notes.ApiModel;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Domain.Users;
using GoogleKeep.IntegrationTests.SeedWork;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace GoogleKeep.Tests.Notes
{

    public class NotesControllerTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task PostShouldRespondWithBadRequestWhenTitleIsEmptyOrWhitespace(string title)
        {
            // given
            var httpClient = new NotesWebApplicationFactory().CreateClient();

            // when
            var response = await httpClient.PostAsJsonAsync("/api/notes", new CreateNoteRequest() { Title = title });

            // then
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostShouldRespondWithLocationHeader()
        {
            // given
            const string noteTitle = "Lorem ipsum";
            var httpClient = new NotesWebApplicationFactory().CreateClient();

            // when
            var response = await httpClient.PostAsJsonAsync("/api/notes", new CreateNoteRequest() { Title = noteTitle });

            // then
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);
        }

        [Fact]
        public async Task GetShouldRespondWithNotFoundWhenNoteDoesNotExist()
        {
            // given
            var httpClient = new NotesWebApplicationFactory().CreateClient();

            // when
            var response = await httpClient.GetAsync($"/api/notes/{Guid.NewGuid()}");

            // then
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetShouldRespondWithNoteDtoWhenCurrentIsAnOwner()
        {
            // given
            var guid = Guid.NewGuid();
            var owner = new User(new UserId(guid));
            var note = Note.Create("Test note", owner);

            var httpClient = new NotesWebApplicationFactory()
                .WithAuthenticatedUser(guid)
                .WithNote(note)
                .CreateClient();

            // when
            var response = await httpClient.GetAsync($"/api/notes/{note.Id.Value}");

            // then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var noteDto = await response.Content.ReadFromJsonAsync<NoteDto>();
            Assert.NotNull(noteDto);
            Assert.Equal(note.Id.Value, noteDto.Id);
            Assert.Equal(note.Title, noteDto.Title);
        }

        [Fact]
        public async Task GetShouldRespondWithForbiddenWhenCurrentUserIsNotAnOwner()
        {
            // given
            var owner = new User(new UserId(Guid.NewGuid()));
            var note = Note.Create("Test note", owner);

            var httpClient = new NotesWebApplicationFactory()
                .WithAuthenticatedUser(Guid.NewGuid())
                .WithNote(note)
                .CreateClient();

            // when
            var response = await httpClient.GetAsync($"/api/notes/{note.Id.Value}");

            // then
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
