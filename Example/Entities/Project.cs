namespace Example.Entities
{
    public class Project
    {
        public const int STATUS_INIT_PROJECT = 0;
        public const int STATUS_DESIGN_REQUEST = 1;
        public const int STATUS_DESIGNER = 2;
        public const int STATUS_REP_DESIGN_REVIEW = 3;
        public const int STATUS_SPEC_CHECK = 4;
        public const int STATUS_WDI = 5;
        public const int STATUS_COMPLETED = 6;
        public const int STATUS_CLOSED = 7;

        public int Id { get; set; }
        public int? CreatorUserId { get; set; }
        public User Creator { get; set; }
        public string Title { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? SpaceId { get; set; }
        public Space Space { get; set; }
        public int? TaskId { get; set; }
        public int Type { get; set; }
        public bool IsLead { get; set; }
        public int Status { get; set; }
        public int PrivateStatus { get; set; }
        public int IsBidTeam { get; set; }
        public decimal? EstimatedOpportunitySize { get; set; }
        public string LeadSource { get; set; }
        public string Competitor { get; set; }
        public DateTime? ContactDate { get; set; }
        public DateTime? DesiredCompletionDate { get; set; }
        public string SiteAddress { get; set; }
        public string Description { get; set; }
        public bool IsSendNewCustomer { get; set; }
        public string DdmsSalesOrderId { get; set; }
        public int? DdmsSyncStatus { get; set; }
        public DateTime? DdmsSyncLastDate { get; set; }

        // BORRAR
        public string Files { get; set; }
        public string Ticket { get; set; }
        public string ZipCode { get; set; }
        public List<ProjectPurchaseOrder> Orders { get; set; }
        public List<ProjectAcknolegment> Acknolegments { get; set; }
    }
}
