namespace school.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DOB { get; set; }
        public string Residence { get; set; }
        public ICollection<Subject> subjects { get; set; }= new HashSet<Subject>();

    }
}
