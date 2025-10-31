using BuyBuddy.Marketplace.Application.Abstractions.Repositories;
using BuyBuddy.Marketplace.Domain.Common.BaseEntities;
using BuyBuddy.Marketplace.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Linq.Expressions;

namespace BuyBuddy.Marketplace.Persistence.Concretes.Repositories.SQL.EFCore
{
    public abstract class EfReadRepository<TEntity, TPrimaryKey, TTimestamp, TStatus> :
        IReadRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>
        where TEntity : BaseEntity<TPrimaryKey, TTimestamp, TStatus>, new()
        where TPrimaryKey : struct
        where TTimestamp : struct
        where TStatus : Enum
    {
        protected readonly DbContext _context;
        protected readonly ILogger<EfReadRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>> _logger;

        protected EfReadRepository(DbContext context,
        ILogger<EfReadRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>> logger)
        {
            _context = context;
            _logger = logger;
        }

        protected IQueryable<TEntity> Query(bool isTracking) => isTracking
            ? _context.Set<TEntity>()
            : _context.Set<TEntity>().AsNoTracking();

        public virtual async Task<TEntity?> GetByUidAsync(
            TPrimaryKey uid,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("UID", uid))
            {
                try
                {
                    _logger.LogInformation("Fetching entity {EntityName} with UID {UID}", typeof(TEntity).Name, uid);

                    var entity = await Query(isTracking)
                        .SingleOrDefaultAsync(e => e.Uid!.Equals(uid), cancellationToken);

                    if (entity == null)
                        _logger.LogWarning("{EntityName} not found with UID {UID}", typeof(TEntity).Name, uid);

                    return entity;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual TEntity? GetByUid(
            TPrimaryKey uid,
            bool isTracking = false)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("UID", uid))
            {
                try
                {
                    _logger.LogInformation("Fetching entity {EntityName} with UID {UID}", typeof(TEntity).Name, uid);

                    var entity = Query(isTracking)
                        .SingleOrDefault(e => e.Uid!.Equals(uid));

                    if (entity == null)
                        _logger.LogWarning("{EntityName} not found with UID {UID}", typeof(TEntity).Name, uid);

                    return entity;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual async Task<List<TEntity>> GetAllAsync(
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Fetching all entities of type {EntityName}", typeof(TEntity).Name);

                    var entities = await Query(isTracking).ToListAsync(cancellationToken);

                    if (entities == null || entities.Count == 0)
                        _logger.LogWarning("No entities found for {EntityName}", typeof(TEntity).Name);

                    return entities;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual List<TEntity> GetAll(
            bool isTracking = false)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Fetching all entities of type {EntityName}", typeof(TEntity).Name);

                    var entities = Query(isTracking).ToList();

                    if (entities == null || entities.Count == 0)
                        _logger.LogWarning("No entities found for {EntityName}", typeof(TEntity).Name);

                    return entities;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual async Task<TEntity?> GetByConditionAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Fetching entity {EntityName} with condition", typeof(TEntity).Name);

                    var entity = await Query(isTracking)
                        .FirstOrDefaultAsync(predicate, cancellationToken);

                    if (entity == null)
                        _logger.LogWarning("No {EntityName} found matching condition", typeof(TEntity).Name);

                    return entity;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual TEntity? GetByCondition(
            Expression<Func<TEntity, bool>> predicate,
            bool isTracking = false)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Fetching entity {EntityName} with condition", typeof(TEntity).Name);

                    var entity = Query(isTracking)
                        .FirstOrDefault(predicate);

                    if (entity == null)
                        _logger.LogWarning("No {EntityName} found matching condition", typeof(TEntity).Name);

                    return entity;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual async Task<List<TEntity>> GetWhereAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Fetching entities of type {EntityName} with condition", typeof(TEntity).Name);

                    var entities = await Query(isTracking)
                        .Where(predicate)
                        .ToListAsync(cancellationToken);

                    if (entities.Count == 0)
                        _logger.LogWarning("No entities found for {EntityName} matching condition", typeof(TEntity).Name);

                    return entities;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual List<TEntity> GetWhere(
            Expression<Func<TEntity, bool>> predicate,
            bool isTracking = false)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Fetching entities of type {EntityName} with condition", typeof(TEntity).Name);

                    var entities = Query(isTracking)
                        .Where(predicate)
                        .ToList();

                    if (entities.Count == 0)
                        _logger.LogWarning("No entities found for {EntityName} matching condition", typeof(TEntity).Name);

                    return entities;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual async Task<int> CountAsync(
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Counting entities of type {EntityName}", typeof(TEntity).Name);

                    var count = await Query(isTracking).CountAsync(cancellationToken);

                    _logger.LogInformation("{Count} entities found for {EntityName}", count, typeof(TEntity).Name);

                    return count;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual int Count(bool isTracking = false)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Counting entities of type {EntityName}", typeof(TEntity).Name);

                    var count = Query(isTracking).Count();

                    _logger.LogInformation("{Count} entities found for {EntityName}", count, typeof(TEntity).Name);

                    return count;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual async Task<int> CountWhereAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Counting entities of type {EntityName} with filter", typeof(TEntity).Name);

                    var count = await Query(isTracking).Where(predicate).CountAsync(cancellationToken);

                    _logger.LogInformation("{Count} entities found for {EntityName} with filter", count, typeof(TEntity).Name);

                    return count;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual int CountWhere(
            Expression<Func<TEntity, bool>> predicate,
            bool isTracking = false)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Counting entities of type {EntityName} with filter", typeof(TEntity).Name);

                    var count = Query(isTracking).Where(predicate).Count();

                    _logger.LogInformation("{Count} entities found for {EntityName} with filter", count, typeof(TEntity).Name);

                    return count;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual async Task<bool> ExistAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Checking existence of entities of type {EntityName} with filter", typeof(TEntity).Name);

                    var exists = await Query(isTracking).AnyAsync(predicate, cancellationToken);

                    _logger.LogInformation("Existence check for {EntityName} returned {Exists}", typeof(TEntity).Name, exists);

                    return exists;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
        public virtual bool Exist(
            Expression<Func<TEntity, bool>> predicate,
            bool isTracking = false)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            {
                try
                {
                    _logger.LogInformation("Checking existence of entities of type {EntityName} with filter", typeof(TEntity).Name);

                    var exists = Query(isTracking).Any(predicate);

                    _logger.LogInformation("Existence check for {EntityName} returned {Exists}", typeof(TEntity).Name, exists);

                    return exists;
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    throw;
                }
            }
        }
    }
}
