using BuyBuddy.Marketplace.Domain.Common.BaseEntities;
using System.Linq.Expressions;

namespace BuyBuddy.Marketplace.Application.Abstractions.Repositories
{
    public interface IReadRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>
        where TEntity : BaseEntity<TPrimaryKey, TTimestamp, TStatus>, new()
        where TPrimaryKey : struct
        where TTimestamp : struct
        where TStatus : Enum
    {
        Task<TEntity?> GetByUidAsync(TPrimaryKey uid, bool isTracking = false, CancellationToken cancellationToken = default);
        TEntity? GetByUid(TPrimaryKey uid, bool isTracking = false);
        Task<List<TEntity>> GetAllAsync(bool isTracking = false, CancellationToken cancellationToken = default);
        List<TEntity> GetAll(bool isTracking = false);
        Task<TEntity?> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default);
        TEntity? GetByCondition(Expression<Func<TEntity, bool>> predicate, bool isTracking = false);
        Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default);
        List<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, bool isTracking = false);
        Task<int> CountAsync(bool isTracking = false, CancellationToken cancellationToken = default);
        int Count(bool isTracking = false);
        Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default);
        int CountWhere(Expression<Func<TEntity, bool>> predicate, bool isTracking = false);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, CancellationToken cancellationToken = default);
        bool Exist(Expression<Func<TEntity, bool>> predicate, bool isTracking = false);
    }
}
