using BuyBuddy.Marketplace.Domain.Common.BaseEntities;

namespace BuyBuddy.Marketplace.Application.Abstractions.Repositories
{
    public interface IWriteRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>
        where TEntity : BaseEntity<TPrimaryKey, TTimestamp, TStatus>, new()
        where TPrimaryKey : struct
        where TTimestamp : struct
        where TStatus : Enum
    {
        Task<(bool IsAdded, TEntity? Entity)> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        (bool IsAdded, TEntity? Entity) Add(TEntity entity);
        Task<(bool IsAdded, List<TEntity> Entities)> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        (bool IsAdded, List<TEntity> Entities) AddRange(List<TEntity> entities);
        (bool IsUpdated, TEntity? Entity) Update(TEntity entity);
        (bool IsUpdated, List<TEntity> Entities) UpdateRange(List<TEntity> entities);
        (bool IsRemoved, TEntity? Entity) Remove(TEntity entity);
        (bool IsRemoved, List<TEntity> Entities) RemoveRange(List<TEntity> entities);
    }
}
