namespace Example.Entities
{
    public class ProjectAcknolegment
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string DdmsId { get; set; }
        public DateTime ShipDate { get; set; }
        public int InstallId { get; set; }
        public ProjectInstall Install { get; set; }
        public string Vendor { get; set; }
        public string VendorPO { get; set; }
        public int Items { get; set; }
        public int Status { get; set; }
    }
}
