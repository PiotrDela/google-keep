using Azure.Data.Tables;
using GoogleKeep.Api.Notes;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Infrastructure.AzureStorage;
using GoogleKeep.Infrastructure.Notes;
using Xunit;

namespace GoogleKeep.IntegrationTests.Notes
{
    public class GetUserNotesQueryHandlerTests
    {
        [Fact]
        public async Task HandleShouldReturnOnlyNotesOwnedByGivenUser()
        {
            var user1 = new User(new UserId(Guid.NewGuid()));
            var user2 = new User(new UserId(Guid.NewGuid()));

            var note1 = Note.Create("Some note", user1);
            var note2 = Note.Create("Some other note", user2);
            
            var connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

            var tableServiceClient = new TableServiceClient(connectionString);

            TableNamingConvention tableNamingConvention = TableNamingConvention.WithSuffix(nameof(GetUserNotesQueryHandlerTests));
            var repository = new AzureTableStorageRepository(tableServiceClient, tableNamingConvention);

            try
            {
                await repository.AddAsync(note1);
                await repository.AddAsync(note2);

                var queryHandler = new GetUserNotesQueryHandler(tableServiceClient, tableNamingConvention);

                var result = await queryHandler.Handle(new GetUserNotesQuery(user1.Id.Value), CancellationToken.None);

                var noteDto = Assert.Single(result);
                Assert.Equal(note1.Id.Value, noteDto.Id);
                Assert.Equal(note1.Title, noteDto.Title);
            }
            finally
            {
                await tableServiceClient.DeleteTableAsync(tableNamingConvention.GetTableName(AzureTableStorageRepository.TableName));
            }
        }
    }
}
