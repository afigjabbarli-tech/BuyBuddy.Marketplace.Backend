namespace BuyBuddy.Marketplace.Domain.Interfaces
{
    public interface IAuditable<TPrimaryKey, TTimestamp>
        where TPrimaryKey : struct
        where TTimestamp : struct
    {
        TPrimaryKey? CreatedBy { get; set; }
        TTimestamp CreationDateAndTime { get; set; }
        TPrimaryKey? ModifiedBy { get; set; }
        TTimestamp? LastModificationDateAndTime { get; set; }
        TPrimaryKey? StatusModifiedBy { get; set; }
        TTimestamp? StatusLastModificationDateAndTime { get; set; }
        TPrimaryKey? DeletedBy { get; set; }
        TTimestamp? DeletionDateAndTime { get; set; }
    }
}
