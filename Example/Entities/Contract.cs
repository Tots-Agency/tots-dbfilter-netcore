namespace Example.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Caption { get; set; }
        public int? Type { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? Status { get; set; }
        public string? DdmsId { get; set; }
        public string? DdmsPricePlan { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? CostType { get; set; }
        public string? ListType { get; set; }
        public string? SetCost { get; set; }
        public string? SetList { get; set; }
    }
}
