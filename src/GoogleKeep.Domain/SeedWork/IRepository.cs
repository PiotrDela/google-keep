namespace GoogleKeep.Domain.SeedWork
{
    public interface IRepository<TEntity> where TEntity: Entity, IAggregateRoot
    {
        void Add(TEntity entity);
    }
}
