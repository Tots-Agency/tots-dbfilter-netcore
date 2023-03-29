namespace Example.Entities
{
    public class Space
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Site Site { get; set; }
        public int SiteId { get; set; }
        public bool StairCarry { get; set; }
        public bool FirstFloor { get; set; }
        public bool Elevator { get; set; }
    }
}
