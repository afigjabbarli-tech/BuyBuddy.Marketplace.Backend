using BuyBuddy.Marketplace.Domain.Enums;

namespace BuyBuddy.Marketplace.Domain.Entities
{
    public class Address
    {
        public string PostalCode { get; set; }
        public AddressType AddressType { get; set; }
        public double? Latitude { get; set; } 
        public double? Longitude { get; set; }
    }
}
