namespace GoogleKeep.IntegrationTests.SeedWork
{
    public class FakeUserContext
    {
        public Guid UserId { get; }

        public FakeUserContext(Guid userId)
        {
            UserId = userId;
        }
    }
}
