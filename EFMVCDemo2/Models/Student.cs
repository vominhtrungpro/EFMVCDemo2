using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFMVCDemo2.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string StudentAddress { get; set; }

        public int StudentAge { get; set; }
        public List<StudentSubject> StudentSubject { get; set; }
    }
}
