namespace Example.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public string Caption { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<UserRole> Roles { get; set; }
        public string AdId { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public string Nickname { get; set; }
        public string HomeTown { get; set; }
        public string FavoriteTeam { get; set; }
        public string FavoriteFood { get; set; }
        public string FavoriteHobby { get; set; }
        public string Pets { get; set; }
        public string Family { get; set; }
        public string LitteKnownFacts { get; set; }
    }
}
