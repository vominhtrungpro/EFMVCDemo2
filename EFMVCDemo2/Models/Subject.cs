using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFMVCDemo2.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public string SubjectDetail { get; set; }
        public List<StudentSubject> StudentSubject { get; set; }

        public List<TeacherSubject> TeacherSubject { get; set; }


    }
}
