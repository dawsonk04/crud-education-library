using DRK.ProgDec.BL.Models;

namespace DRK.ProgDec.BL.Test
{
    [TestClass]
    public class utDegreeType
    {
        [TestMethod]
        public void LoadTest()
        {
            List<DegreeType> items = DegreeTypeManager.Load();
            Assert.AreEqual(3, DegreeTypeManager.Load().Count);
            Assert.AreEqual(9, items[2].Programs.Count);
        }

        [TestMethod]

        public void LoadByIdTest()
        {
            var item = DegreeTypeManager.LoadById(1);
            Assert.AreEqual(1, item.ID);
            Assert.AreEqual(5, item.Programs.Count);

        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = DegreeTypeManager.Insert("Test", ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            DegreeType degreeType = new DegreeType
            {
                Description = "test"
            };
            int results = DegreeTypeManager.Insert(degreeType, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            DegreeType degreeType = DegreeTypeManager.LoadById(3);
            degreeType.Description = "test";
            int results = DegreeTypeManager.Update(degreeType, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = DegreeTypeManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

    }
}
