namespace Example.Entities
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public bool TrucksAccepted { get; set; }
        public bool AfterHour { get; set; }
        public bool BackgroundRequired { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
