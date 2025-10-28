namespace BuyBuddy.Marketplace.Domain.Interfaces
{
    public interface IHasStatus<TStatus>
        where TStatus : Enum
    {
        TStatus Status { get; set; }
    }
}
