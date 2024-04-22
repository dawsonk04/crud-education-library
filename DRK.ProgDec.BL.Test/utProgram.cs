using DRK.ProgDec.BL.Models;

namespace DRK.ProgDec.BL.Test
{
    [TestClass]
    public class utProgram
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(16, ProgramManager.Load().Count);
        }

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = ProgramManager.Insert("Test", 1, "Test", ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            Program program = new Program
            {
                DegreeTypeID = 1,
                Description = "Test",
                ImagePath = "Test"
            };
            int results = ProgramManager.Insert(program, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            Program program = ProgramManager.LoadById(3);
            program.Description = "Test";
            int results = ProgramManager.Update(program, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = ProgramManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

    }
}
