namespace GoogleKeep.Infrastructure.AzureStorage
{
    public class TableNamingConvention
    {
        private readonly string suffix;

        public static TableNamingConvention Default()
        {
            return new TableNamingConvention(string.Empty);
        }

        public static TableNamingConvention WithSuffix(string suffix)
        {
            return new TableNamingConvention(suffix);
        }

        private TableNamingConvention(string suffix)
        {
            this.suffix = suffix ?? string.Empty;
        }

        public string GetTableName(string tableName)
        {
            return $"{tableName}{suffix}";
        }
    }
}
