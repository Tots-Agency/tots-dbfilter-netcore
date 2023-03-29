namespace Example.Entities
{
    public class ProjectAcknolegmentItem
    {
        public int Id { get; set; }
        public int AcknolegmentId { get; set; }
        public ProjectAcknolegment Acknolegment { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int PurchaseOrderId { get; set; }
        public ProjectPurchaseOrder PurchaseOrder { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal GPPercent { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
    }
}
