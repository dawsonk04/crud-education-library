using System.ComponentModel;

namespace DRK.ProgDec.BL.Models
{
    public class Student
    {
        public int ID { get; set; }
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]

        public string? LastName { get; set; }
        [DisplayName("Student Id")]

        public string? StudentId { get; set; }

        public string FullName { get { return LastName + ", " + FirstName; } }

        public List<Advisor> Advisors { get; set; } = new List<Advisor>();




    }
}
