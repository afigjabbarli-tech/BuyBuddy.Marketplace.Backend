namespace BuyBuddy.Marketplace.Domain.Entities
{
    public class PhoneCode
    {
        public string Code { get; set; }
        public string Symbol { get; set; } = "+";
        public string FullCode => $"{Symbol}{Code}";
    }
}
