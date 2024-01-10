namespace GoogleKeep.Domain.Entities
{
    public class User
    {
        public UserId Id { get; }

        public User(UserId id)
        {
            this.Id = id;
        }
    }
}
