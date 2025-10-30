using BuyBuddy.Marketplace.Domain.Common.BaseEntities;

namespace BuyBuddy.Marketplace.Application.Abstractions.Repositories
{
    public interface IWriteRepository<TEntity, TPrimaryKey, TTimestamp, TStatus>
        where TEntity : BaseEntity<TPrimaryKey, TTimestamp, TStatus>, new()
        where TPrimaryKey : struct
        where TTimestamp : struct
        where TStatus : Enum
    {
        
    }
}
