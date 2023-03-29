namespace Example.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool PoRequired { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DdmsId { get; set; }
        public int? AvgLinesPerInvoice { get; set; }
        public decimal? AvgInvoiceAmount { get; set; }
        public int? HitsYTD { get; set; }
        public decimal? SalesYTD { get; set; }
        public decimal? CostYTD { get; set; }
        public decimal? SalesMarginYTD { get; set; }
        public decimal? SalesGPYTD { get; set; }
        public decimal? SalesLastYTD { get; set; }
        public decimal? CostLastYTD { get; set; }
        public decimal? SalesMarginLastYTD { get; set; }
        public decimal? SalesGPLastYTD { get; set; }
        public string DdmsStatus { get; set; }

        public DateTime? FirstOrderDate { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public string DdmsAccount { get; set; }
        public string Department { get; set; }

        public string SalesPerson { get; set; }
        public string SalesPersonName { get; set; }
        public string SalesPersonSecondary { get; set; }
        public string SalesPersonSecondaryName { get; set; }
        public string Route { get; set; }
        public string ManifestRoute { get; set; }
        public int? CreditLimit { get; set; }
        public string Remarks { get; set; }
        public string SIC { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Category4 { get; set; }
        public string GroupAccount { get; set; }
        public string PricingTaxable { get; set; }
        public string PricingTaxDistrict { get; set; }
        public string PricingDiscountType { get; set; }
        public decimal? PricingDiscountPercent { get; set; }
        public string PricingPricePlan1 { get; set; }
        public string PricingPricePlan2 { get; set; }
        public string PricingPricePlan3 { get; set; }
        public string PricingPricePlan4 { get; set; }
        public string PricingPORequired { get; set; }
        public string PricingHoldDiscount { get; set; }
        public int? PricingHoldDiscountDays { get; set; }
        public string PricingCatalogPrice { get; set; }
        public string PricingBestPricing { get; set; }
        public string PricingCostCode { get; set; }
        public string PricingColumnBreaks { get; set; }
        public decimal? ARCurrent { get; set; }
        public decimal? AROver30 { get; set; }
        public decimal? AROver60 { get; set; }
        public decimal? AROver90 { get; set; }
        public string SSLM { get; set; }
        public string BillToAddress { get; set; }
        public string ContactBilling { get; set; }
        public string ShipToAddress { get; set; }
        public string ContactShipping { get; set; }
        public string CreditReferences { get; set; }
        public string CreditRequested { get; set; }
        public string Notes { get; set; }
    }
}
