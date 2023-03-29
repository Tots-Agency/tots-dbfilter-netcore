namespace Example.Entities
{
    public class ProjectInstall
    {
        public const int STATUS_PENDING = 0;
        public const int STATUS_INPROGRESS = 1;
        public const int STATUS_READY_TO_SHIP = 2;
        public const int STATUS_TRUCK_BOM_COMPLETED = 3;
        public const int STATUS_ITEMS_TO_INSTALL_COMPLETED = 4;
        public const int STATUS_CUSTOMER_BOM_COMPLETED = 5;
        public const int STATUS_PHOTOS_COMPLETE = 6;
        public const int STATUS_COMPLETED = 7;
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public DateTime InstallDate { get; set; }
        public DateTime InstallDateEnd { get; set; }

        public int PurchaseOrders { get; set; }
        public int Vendors { get; set; }
        public string Route { get; set; }
        public string Vehicle { get; set; }
        public int Items { get; set; }
        public int Punchs { get; set; }
        public decimal Hours { get; set; }

        public int? DriverId { get; set; }
        public User Driver { get; set; }

        public int? TruckId { get; set; }

        public string Description { get; set; }

        public string Photos { get; set; }
        public string SignaturePhoto { get; set; }
        public DateTime? SignatureDate { get; set; }

        public virtual List<ProjectAcknolegment> Acknolegments { get; set; }

    }
}
