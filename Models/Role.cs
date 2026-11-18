using school.Models.Enums;

namespace school.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public RoleEnum Name { get; set; }
        public string Description { get; set; }
        public DateOnly CreatedDate { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
