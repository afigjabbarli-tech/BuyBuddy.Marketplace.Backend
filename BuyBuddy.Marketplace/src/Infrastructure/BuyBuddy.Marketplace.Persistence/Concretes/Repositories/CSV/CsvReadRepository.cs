using BuyBuddy.Marketplace.Application.Abstractions.Repositories;
using BuyBuddy.Marketplace.Domain.Common.BaseEntities;
using System.Linq.Expressions;

namespace BuyBuddy.Marketplace.Persistence.Concretes.Repositories.CSV
{
    public class CsvReadRepository<TEntity, TPrimaryKey, TTimestamp, TStatus> :
        IReadRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>
        where TEntity : BaseEntity<TPrimaryKey, TTimestamp, TStatus>, new()
        where TPrimaryKey : struct
        where TTimestamp : struct
        where TStatus : Enum
    {
        public int Count(bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(bool isTracking = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public int CountWhere(Expression<Func<TEntity, bool>> predicate, bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Expression<Func<TEntity, bool>> predicate, bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll(bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAllAsync(bool isTracking = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity? GetByCondition(Expression<Func<TEntity, bool>> predicate, bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity? GetByUid(TPrimaryKey uid, bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetByUidAsync(TPrimaryKey uid, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
