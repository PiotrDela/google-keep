using GoogleKeep.Domain.Entities;

namespace GoogleKeep.Infrastructure.AzureStorage
{
    public static class NotePartitioningStrategy
    {
        public static string GetPartitonKey()
        {
            return nameof(Note);
        }

        public static string GetRowKey(Note note)
        {
            return GetRowKey(note.Id);
        }

        public static string GetRowKey(NoteId noteId)
        {
            return noteId.Value.ToString();
        }
    }
}
