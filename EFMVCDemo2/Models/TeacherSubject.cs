using System.ComponentModel.DataAnnotations;

namespace EFMVCDemo2.Models
{
    public class TeacherSubject
    {
        [Key]
        public int Id { get; set; }
        public Teacher teacher { get; set; }

        public int TeacherId { get; set; }
        public Subject subject { get; set; }

        public int SubjectId { get; set; }
    }
}
