namespace GoogleKeep.Domain.Users
{
    public class User
    {
        public UserId Id { get; }

        public User(UserId id)
        {
            Id = id;
        }
    }
}
