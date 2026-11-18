using school.Models.Enums;

namespace school.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string regNumber { get; set; }
        public string Fullname { get; set; }
        public int Grade { get; set; }
        public StreamEnum Stream { get; set; }
        public int Age { get; set; }
        public DateOnly DOB { get; set; }
        public ICollection<StudentGuardian> StudentGuardian { get; set; } = new HashSet<StudentGuardian>();
    }
}
