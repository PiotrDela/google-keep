using Azure.Data.Tables;
using GoogleKeep.Domain.Entities;
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
            var connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

            var repository = new AzureTableStorageRepository(new TableServiceClient(connectionString), TableNamingConvention.WithSuffix("Tests"));
            await repository.AddAsync(note);

            // then
            var fetchedNote = await repository.GetAsync(note.Id);
            Assert.Equal(noteAsJson, JsonSerializer.Serialize(fetchedNote));
        }
    }
}
