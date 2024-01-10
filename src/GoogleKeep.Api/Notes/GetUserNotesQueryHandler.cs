using Azure;
using Azure.Data.Tables;
using GoogleKeep.Api.Notes.ApiModel;
using GoogleKeep.Api.Queries;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Infrastructure.AzureStorage;
using GoogleKeep.Infrastructure.Notes;
using System.Text.Json;

namespace GoogleKeep.Api.Notes
{
    public class GetUserNotesQueryHandler : IQueryHandler<GetUserNotesQuery, IEnumerable<NoteDto>>
    {
        private readonly TableClient tableClient;

        public GetUserNotesQueryHandler(TableServiceClient tableServiceClient, TableNamingConvention tableNamingConvention)
        {
            this.tableClient = tableServiceClient.GetTableClient(tableNamingConvention.GetTableName(AzureTableStorageRepository.TableName));
        }

        public async Task<IEnumerable<NoteDto>> Handle(GetUserNotesQuery request, CancellationToken cancellationToken)
        {
            var partitionKey = NotePartitioningStrategy.GetPartitonKey();

            var queryResult = this.tableClient.QueryAsync<TableEntity>($"PartitionKey eq '{partitionKey}' and {nameof(Note.Owner)} eq guid'{request.UserId}'", 10, null);

            var dtos = new List<NoteDto>();

            await foreach (Page<TableEntity> page in queryResult.AsPages())
            {
                foreach (TableEntity entity in page.Values)
                {
                    var noteJson = (string)entity["Json"];
                    var note = JsonSerializer.Deserialize<Note>(noteJson);

                    dtos.Add(new NoteDto
                    {
                        Id = note.Id.Value,
                        Title = note.Title
                    });
                }
            }

            return dtos;
        }
    }
}
