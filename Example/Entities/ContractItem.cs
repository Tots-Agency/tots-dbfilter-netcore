namespace Example.Entities
{
    public class ContractItem
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
}
