using DRK.ProgDec.BL.Models;

namespace DRK.ProgDec.BL.Test
{
    [TestClass]
    public class utStudent
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(5, StudentManager.Load().Count);
        }

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = StudentManager.Insert("Bale", "Organa", "5555555555", ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            Student student = new Student
            {
                FirstName = "test",
                LastName = "test",
                StudentId = "test"
            };
            int results = StudentManager.Insert(student, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            Student student = StudentManager.LoadById(3);
            student.FirstName = "test";
            int results = StudentManager.Update(student, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = StudentManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

    }
}
