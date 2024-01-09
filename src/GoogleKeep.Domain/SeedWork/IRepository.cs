namespace GoogleKeep.Domain.SeedWork
{
    public interface IRepository<TEntity> where TEntity: Entity, IAggregateRoot
    {
        Task AddAsync(TEntity entity);
    }
}
