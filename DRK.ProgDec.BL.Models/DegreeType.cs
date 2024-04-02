namespace DRK.ProgDec.BL.Models
{
    public class DegreeType
    {
        public int ID { get; set; }
        public string? Description { get; set; }

        public List<Program> Programs { get; set; } = new List<Program>();
    }


}
