using BuyBuddy.Marketplace.Application.Abstractions.Repositories;
using BuyBuddy.Marketplace.Domain.Common.BaseEntities;
using BuyBuddy.Marketplace.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace BuyBuddy.Marketplace.Persistence.Concretes.Repositories.SQL.EFCore
{
    public abstract class EfWriteRepository<TEntity, TPrimaryKey, TTimestamp, TStatus> :
        IWriteRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>
        where TEntity : BaseEntity<TPrimaryKey, TTimestamp, TStatus>, new()
        where TPrimaryKey : struct
        where TTimestamp : struct
        where TStatus : Enum
    {
        protected readonly DbContext _context;
        protected readonly ILogger<EfWriteRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>> _logger;

        protected EfWriteRepository(DbContext context,
            ILogger<EfWriteRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>> logger)
        {
            _context = context;
            _logger = logger;
        }

        protected DbSet<TEntity> Table => _context.Set<TEntity>();

        public virtual async Task<(bool IsAdded, TEntity? Entity)> AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("Operation", "Add"))
            {
                try
                {
                    _logger.LogInformation("Starting Add operation for entity {EntityName}", typeof(TEntity).Name);

                    if (entity == null)
                    {
                        _logger.LogWarning("Attempted to add a null entity of type {EntityName}", typeof(TEntity).Name);
                        return (false, entity);
                    }

                    EntityEntry<TEntity> entry = await Table.AddAsync(entity, cancellationToken);
                    bool isAdded = entry.State == EntityState.Added;

                    if (isAdded)
                    {
                        _logger.LogInformation(
                            "Entity {EntityName} successfully added to ChangeTracker with state {State}",
                            typeof(TEntity).Name, entry.State);
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Entity {EntityName} could not be marked as Added in ChangeTracker. Current state: {State}",
                            typeof(TEntity).Name, entry.State);
                    }

                    _logger.LogInformation("Completed Add operation for entity {EntityName} with result: {Result}",
                        typeof(TEntity).Name, isAdded ? "Success" : "Failure");

                    return (isAdded, entity);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    _logger.LogError("Error occurred during Add operation for {EntityName}", typeof(TEntity).Name);
                    throw;
                }
            }
        }
        public virtual (bool IsAdded, TEntity? Entity) Add(TEntity entity)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("Operation", "Add"))
            {
                try
                {
                    _logger.LogInformation("Starting Add operation for entity {EntityName}", typeof(TEntity).Name);

                    if (entity == null)
                    {
                        _logger.LogWarning("Attempted to add a null entity of type {EntityName}", typeof(TEntity).Name);
                        return (false, entity);
                    }

                    EntityEntry<TEntity> entry = Table.Add(entity);
                    bool isAdded = entry.State == EntityState.Added;

                    if (isAdded)
                    {
                        _logger.LogInformation(
                            "Entity {EntityName} successfully added to ChangeTracker with state {State}",
                            typeof(TEntity).Name, entry.State);
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Entity {EntityName} could not be marked as Added in ChangeTracker. Current state: {State}",
                            typeof(TEntity).Name, entry.State);
                    }

                    _logger.LogInformation(
                        "Completed Add operation for entity {EntityName} with result: {Result}",
                        typeof(TEntity).Name, isAdded ? "Success" : "Failure");

                    return (isAdded, entity);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    _logger.LogError("Error occurred during Add operation for {EntityName}", typeof(TEntity).Name);
                    throw;
                }
            }
        }
        public virtual async Task<(bool IsAdded, List<TEntity> Entities)> AddRangeAsync(
            List<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("Operation", "AddRange"))
            {
                try
                {
                    _logger.LogInformation("Starting AddRange operation for entity type {EntityName}", typeof(TEntity).Name);

                    if (entities == null || !entities.Any())
                    {
                        _logger.LogWarning("Attempted to add an empty or null list of entities for type {EntityName}", typeof(TEntity).Name);
                        return (false, entities ?? new List<TEntity>());
                    }

                    await Table.AddRangeAsync(entities, cancellationToken);
                    bool allAdded = entities.All(e => Table.Entry(e).State == EntityState.Added);

                    if (allAdded)
                    {
                        _logger.LogInformation(
                            "All {Count} entities of type {EntityName} successfully added to ChangeTracker with state {State}",
                            entities.Count, typeof(TEntity).Name, EntityState.Added);
                    }
                    else
                    {
                        var notAdded = entities
                            .Where(e => Table.Entry(e).State != EntityState.Added)
                            .Select(e => Table.Entry(e).State)
                            .ToList();

                        _logger.LogWarning(
                            "Some entities of type {EntityName} were not marked as Added. Non-added states: {States}",
                            typeof(TEntity).Name, string.Join(", ", notAdded));
                    }

                    _logger.LogInformation("Completed AddRange operation for entity type {EntityName} with result: {Result}",
                        typeof(TEntity).Name, allAdded ? "Success" : "Partial/Failure");

                    return (allAdded, entities);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    _logger.LogError("Error occurred during AddRange operation for entity type {EntityName}", typeof(TEntity).Name);
                    throw;
                }
            }
        }
        public virtual (bool IsAdded, List<TEntity> Entities) AddRange(List<TEntity> entities)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("Operation", "AddRange"))
            {
                try
                {
                    _logger.LogInformation("Starting AddRange operation for entity type {EntityName}", typeof(TEntity).Name);

                    if (entities == null || !entities.Any())
                    {
                        _logger.LogWarning("Attempted to add an empty or null list of entities for type {EntityName}", typeof(TEntity).Name);
                        return (false, entities ?? new List<TEntity>());
                    }

                    Table.AddRange(entities);
                    bool allAdded = entities.All(e => Table.Entry(e).State == EntityState.Added);

                    if (allAdded)
                    {
                        _logger.LogInformation(
                            "All {Count} entities of type {EntityName} successfully added to ChangeTracker with state {State}",
                            entities.Count, typeof(TEntity).Name, EntityState.Added);
                    }
                    else
                    {
                        var notAdded = entities
                            .Where(e => Table.Entry(e).State != EntityState.Added)
                            .Select(e => Table.Entry(e).State)
                            .ToList();

                        _logger.LogWarning(
                            "Some entities of type {EntityName} were not marked as Added. Non-added states: {States}",
                            typeof(TEntity).Name, string.Join(", ", notAdded));
                    }

                    _logger.LogInformation("Completed AddRange operation for entity type {EntityName} with result: {Result}",
                        typeof(TEntity).Name, allAdded ? "Success" : "Partial/Failure");

                    return (allAdded, entities);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    _logger.LogError("Error occurred during AddRange operation for entity type {EntityName}", typeof(TEntity).Name);
                    throw;
                }
            }
        }
        public virtual (bool IsUpdated, TEntity? Entity) Update(TEntity entity)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("Operation", "Update"))
            {
                try
                {
                    _logger.LogInformation("Starting Update operation for entity {EntityName}", typeof(TEntity).Name);

                    if (entity == null)
                    {
                        _logger.LogWarning("Attempted to update a null entity of type {EntityName}", typeof(TEntity).Name);
                        return (false, entity);
                    }

                    EntityEntry<TEntity> entry = Table.Update(entity);
                    bool isUpdated = entry.State == EntityState.Modified;

                    if (isUpdated)
                    {
                        _logger.LogInformation(
                            "Entity {EntityName} successfully marked as Modified in ChangeTracker",
                            typeof(TEntity).Name);
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Entity {EntityName} could not be marked as Modified. Current state: {State}",
                            typeof(TEntity).Name, entry.State);
                    }

                    _logger.LogInformation(
                        "Completed Update operation for entity {EntityName} with result: {Result}",
                        typeof(TEntity).Name, isUpdated ? "Success" : "Failure");

                    return (isUpdated, entity);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    _logger.LogError("Error occurred during Update operation for entity {EntityName}", typeof(TEntity).Name);
                    throw;
                }
            }
        }
        public virtual (bool IsUpdated, List<TEntity> Entities) UpdateRange(List<TEntity> entities)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("Operation", "UpdateRange"))
            {
                try
                {
                    _logger.LogInformation("Starting UpdateRange operation for entity type {EntityName}", typeof(TEntity).Name);

                    if (entities == null || !entities.Any())
                    {
                        _logger.LogWarning("Attempted to update an empty or null list of entities for type {EntityName}", typeof(TEntity).Name);
                        return (false, entities ?? new List<TEntity>());
                    }

                    Table.UpdateRange(entities);
                    bool allUpdated = entities.All(e => Table.Entry(e).State == EntityState.Modified);

                    if (allUpdated)
                    {
                        _logger.LogInformation(
                            "All {Count} entities of type {EntityName} successfully marked as Modified in ChangeTracker",
                            entities.Count, typeof(TEntity).Name);
                    }
                    else
                    {
                        var notUpdatedStates = entities
                            .Where(e => Table.Entry(e).State != EntityState.Modified)
                            .Select(e => Table.Entry(e).State)
                            .ToList();

                        _logger.LogWarning(
                            "Some entities of type {EntityName} were not marked as Modified. Non-modified states: {States}",
                            typeof(TEntity).Name, string.Join(", ", notUpdatedStates));
                    }

                    _logger.LogInformation("Completed UpdateRange operation for entity type {EntityName} with result: {Result}",
                        typeof(TEntity).Name, allUpdated ? "Success" : "Partial/Failure");

                    return (allUpdated, entities);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    _logger.LogError("Error occurred during UpdateRange operation for entity type {EntityName}", typeof(TEntity).Name);
                    throw;
                }
            }
        }
        public virtual (bool IsRemoved, TEntity? Entity) Remove(TEntity entity)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("Operation", "Remove"))
            {
                try
                {
                    _logger.LogInformation("Starting Remove operation for entity {EntityName}", typeof(TEntity).Name);

                    if (entity == null)
                    {
                        _logger.LogWarning("Attempted to remove a null entity of type {EntityName}", typeof(TEntity).Name);
                        return (false, entity);
                    }

                    EntityEntry<TEntity> entry = Table.Remove(entity);
                    bool isRemoved = entry.State == EntityState.Deleted;

                    if (isRemoved)
                    {
                        _logger.LogInformation(
                            "Entity {EntityName} successfully marked as Deleted in ChangeTracker",
                            typeof(TEntity).Name);
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Entity {EntityName} could not be marked as Deleted. Current state: {State}",
                            typeof(TEntity).Name, entry.State);
                    }

                    _logger.LogInformation(
                        "Completed Remove operation for entity {EntityName} with result: {Result}",
                        typeof(TEntity).Name, isRemoved ? "Success" : "Failure");

                    return (isRemoved, entity);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    _logger.LogError("Error occurred during Remove operation for entity {EntityName}", typeof(TEntity).Name);
                    throw;
                }
            }
        }
        public virtual (bool IsRemoved, List<TEntity> Entities) RemoveRange(List<TEntity> entities)
        {
            using (LogContext.PushProperty("EntityName", typeof(TEntity).Name))
            using (LogContext.PushProperty("Operation", "RemoveRange"))
            {
                try
                {
                    _logger.LogInformation("Starting RemoveRange operation for entity type {EntityName}", typeof(TEntity).Name);

                    if (entities == null || !entities.Any())
                    {
                        _logger.LogWarning("Attempted to remove an empty or null list of entities for type {EntityName}", typeof(TEntity).Name);
                        return (false, entities ?? new List<TEntity>());
                    }

                    Table.RemoveRange(entities);
                    bool allRemoved = entities.All(e => Table.Entry(e).State == EntityState.Deleted);

                    if (allRemoved)
                    {
                        _logger.LogInformation(
                            "All {Count} entities of type {EntityName} successfully marked as Deleted in ChangeTracker",
                            entities.Count, typeof(TEntity).Name);
                    }
                    else
                    {
                        var notRemovedStates = entities
                            .Where(e => Table.Entry(e).State != EntityState.Deleted)
                            .Select(e => Table.Entry(e).State)
                            .ToList();

                        _logger.LogWarning(
                            "Some entities of type {EntityName} were not marked as Deleted. Non-deleted states: {States}",
                            typeof(TEntity).Name, string.Join(", ", notRemovedStates));
                    }

                    _logger.LogInformation("Completed RemoveRange operation for entity type {EntityName} with result: {Result}",
                        typeof(TEntity).Name, allRemoved ? "Success" : "Partial/Failure");

                    return (allRemoved, entities);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogDetailedError(_logger, ex);
                    _logger.LogError("Error occurred during RemoveRange operation for entity type {EntityName}", typeof(TEntity).Name);
                    throw;
                }
            }
        }
    }
}
