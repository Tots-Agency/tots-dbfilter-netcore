namespace Example.Entities
{
    public class ProjectPurchaseOrder
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string DdmsId { get; set; }
        public string DdmsNumber { get; set; }

        public decimal TotalSell { get; set; }
        public decimal TotalCost { get; set; }
        public decimal GPPercent { get; set; }

        public DateTime? OrderDate { get; set; }
        public int Status { get; set; }
    }
}
