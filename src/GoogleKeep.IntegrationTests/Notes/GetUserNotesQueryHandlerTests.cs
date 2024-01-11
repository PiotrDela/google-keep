using Azure.Data.Tables;
using GoogleKeep.Api.Notes;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Domain.Users;
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
            
            var connectionString = "";

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
