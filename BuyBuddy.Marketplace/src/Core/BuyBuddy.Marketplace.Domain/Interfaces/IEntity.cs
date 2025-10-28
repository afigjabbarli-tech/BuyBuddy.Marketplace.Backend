namespace BuyBuddy.Marketplace.Domain.Interfaces
{
    public interface IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        TPrimaryKey Uid { get; set; }
    }
}
