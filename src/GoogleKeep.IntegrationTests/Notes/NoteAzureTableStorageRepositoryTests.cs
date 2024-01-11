using Azure.Data.Tables;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Domain.Users;
using GoogleKeep.Infrastructure.AzureStorage;
using GoogleKeep.Infrastructure.Notes;
using System.Text.Json;
using Xunit;

namespace GoogleKeep.IntegrationTests.Notes
{
    public class NoteAzureTableStorageRepositoryTests
    {
        [Fact]
        public async Task PersistingNoteInTableStorageShouldWork()
        {
            // given
            var owner = new User(new UserId(Guid.NewGuid()));
            var note = Note.Create("Note to be saved in storage", owner);
            var noteAsJson = JsonSerializer.Serialize(note);

            // when
            var connectionString = "";

            var repository = new AzureTableStorageRepository(new TableServiceClient(connectionString), TableNamingConvention.WithSuffix("Tests"));
            await repository.AddAsync(note);

            // then
            var fetchedNote = await repository.GetAsync(note.Id);
            Assert.Equal(noteAsJson, JsonSerializer.Serialize(fetchedNote));
        }
    }
}
