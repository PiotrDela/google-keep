using Azure.Data.Tables;
using GoogleKeep.Domain.Entities;
using GoogleKeep.Infrastructure.AzureStorage;
using System.Text.Json;

namespace GoogleKeep.Infrastructure.Notes
{
    public class AzureTableStorageRepository : INoteRepository
    {
        private const string TableName = nameof(Note);
        private const string TableEntityPropertyName = $"{nameof(Note)}Json";

        private readonly TableClient tableClient;

        public AzureTableStorageRepository(TableServiceClient tableServiceClient, TableNamingConvention tableNamingConvention)
        {
            this.tableClient = tableServiceClient.GetTableClient(tableNamingConvention.GetTableName(TableName));
            this.tableClient.CreateIfNotExists();
        }

        public async Task AddAsync(Note entity)
        {
            var partitionKey = NotePartitioningStrategy.GetPartitonKey();
            var rowKey = NotePartitioningStrategy.GetRowKey(entity);

            var tableEntity = new TableEntity(partitionKey, rowKey)
            {
                [TableEntityPropertyName] = JsonSerializer.Serialize(entity)
            };

            await this.tableClient.AddEntityAsync(tableEntity);
        }

        public async Task<Note> GetAsync(NoteId noteId)
        {
            var partitionKey = NotePartitioningStrategy.GetPartitonKey();
            var rowKey = NotePartitioningStrategy.GetRowKey(noteId);

            var tableEntity = await tableClient.GetEntityAsync<TableEntity>(partitionKey, rowKey);
            var noteJson = (string)tableEntity.Value[TableEntityPropertyName];

            return JsonSerializer.Deserialize<Note>(noteJson);
        }
    }
}
