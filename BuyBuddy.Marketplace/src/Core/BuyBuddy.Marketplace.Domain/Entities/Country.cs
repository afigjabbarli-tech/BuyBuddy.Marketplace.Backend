namespace BuyBuddy.Marketplace.Domain.Entities
{
    public class Country
    {
        public string CommonName { get; set; }
        public string OfficialName { get; set; }
        public string NativeName { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string NumericCode { get; set; }
        public string TLD { get; set; }
        public string Overview { get; set; }
        public string Capital { get; set; }
        public string LargestCity { get; set; }
        public string SmallestCity { get; set; }
        public double AreaKm2 { get; set; }
        public long Population { get; set; }
        public double PopulationDensity { get; set; }
        public string Demonym { get; set; }
        public double GDP { get; set; }
    }
}
