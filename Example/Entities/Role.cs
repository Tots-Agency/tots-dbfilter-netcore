namespace Example.Entities
{
    public class Role
    {
        public const int ROLE_SALES = 2360;
        public const int ROLE_SALES_MANAGER = 2412;

        public int Id { get; set; }
        public string Title { get; set; }
        public string AdId { get; set; }
        public List<RolePermission> Persmissions { get; set; }
    }
}
