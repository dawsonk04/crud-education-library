using System.ComponentModel;

namespace DRK.ProgDec.BL.Models
{
    public class Program
    {
        public int ID { get; set; }
        public int DegreeTypeID { get; set; }
        public String? Description { get; set; }

        [DisplayName("Degree Name")]
        public string DegreeTypeName { get; set; }
    }
}
