using System.ComponentModel;

namespace DRK.ProgDec.BL.Models
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Password { get; set; }
        [DisplayName("Full Name")]

        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
