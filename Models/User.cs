namespace school.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateOnly DOB { get; set; }
        public DateOnly CreatedDate { get; set; }
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
    }
}
