namespace school.Models
{
    public class StudentGuardian
    {
        public Guid StudentId { get; set; }
        public Student student { get; set; }
        public Guid GuardianId { get; set; }
        public Guardian guardian { get; set; }
        public string Relationship { get; set; }

        public bool IsPrimaryContact { get; set; }

    }
}
