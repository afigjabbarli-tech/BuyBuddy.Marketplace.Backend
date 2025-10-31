using BuyBuddy.Marketplace.Application.Abstractions.Repositories;
using BuyBuddy.Marketplace.Domain.Common.BaseEntities;

namespace BuyBuddy.Marketplace.Persistence.Concretes.Repositories.TXT
{
    public class TxtWriteRepository<TEntity, TPrimaryKey, TTimestamp, TStatus> :
        IWriteRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>
        where TEntity : BaseEntity<TPrimaryKey, TTimestamp, TStatus>, new()
        where TPrimaryKey : struct
        where TTimestamp : struct
        where TStatus : Enum
    {
        public (bool IsAdded, TEntity? Entity) Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsAdded, TEntity? Entity)> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public (bool IsAdded, List<TEntity> Entities) AddRange(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsAdded, List<TEntity> Entities)> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public (bool IsRemoved, TEntity? Entity) Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public (bool IsRemoved, List<TEntity> Entities) RemoveRange(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public (bool IsUpdated, TEntity? Entity) Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public (bool IsUpdated, List<TEntity> Entities) UpdateRange(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
