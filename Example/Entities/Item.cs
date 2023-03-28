namespace Example.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string MACCode { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public int Location { get; set; }
        public int CurrentStock { get; set; }
        public decimal Cost { get; set; }
        public decimal DdmsId { get; set; }
        public decimal Price { get; set; }
    }
}
