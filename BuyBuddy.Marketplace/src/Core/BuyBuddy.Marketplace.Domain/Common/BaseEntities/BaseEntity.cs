using BuyBuddy.Marketplace.Domain.Interfaces;

namespace BuyBuddy.Marketplace.Domain.Common.BaseEntities
{
    public abstract class BaseEntity<TPrimaryKey, TTimestamp, TStatus> :
        IEntity<TPrimaryKey>,
        IAuditable<TPrimaryKey, TTimestamp>,
        IHasStatus<TStatus>,
        IModifiable,
        ISoftDelete
        where TPrimaryKey : struct
        where TTimestamp : struct
        where TStatus : Enum
    {
        public TPrimaryKey Uid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TPrimaryKey? CreatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TTimestamp CreationDateAndTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TPrimaryKey? ModifiedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TTimestamp? LastModificationDateAndTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TPrimaryKey? StatusModifiedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TTimestamp? StatusLastModificationDateAndTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TPrimaryKey? DeletedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TTimestamp? DeletionDateAndTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TStatus Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsModified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
