namespace DRK.ProgDec.UI.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }

        public List<Advisor> Advisors { get; set; } = new List<Advisor>(); // all of the advisors

        public IEnumerable<int> AdvisorIds { get; set; } // the new advisors for the student

        public StudentVM()
        {
            Advisors = AdvisorManager.Load();
        }

        public StudentVM(int id)
        {
            Advisors = AdvisorManager.Load();
            Student = StudentManager.LoadById(id);
            AdvisorIds = Student.Advisors.Select(a => a.Id);
        }


    }
}
