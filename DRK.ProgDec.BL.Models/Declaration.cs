using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DRK.ProgDec.BL.Models
{
    public class Declaration
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int ProgramID { get; set; }
        [DisplayName("ChangeDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ChangeDate { get; set; }

    }
}
