using DRK.ProgDec.BL.Models;

namespace DRK.ProgDec.BL.Test
{
    [TestClass]
    public class utDeclaration
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(4, DeclarationManager.Load().Count);
        }

        [TestMethod]

        public void InsertTest1()
        {
            int id = 0;
            int results = DeclarationManager.Insert(1, 1, ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]

        public void InsertTest2()
        {
            int result = 0;
            Declaration declaration = new Declaration
            {
                ChangeDate = DateTime.Now
            };
            int results = DeclarationManager.Insert(declaration, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            int result = 0;
            Declaration declaration = DeclarationManager.LoadById(3);
            declaration.ChangeDate = DateTime.Now;
            int results = DeclarationManager.Update(declaration, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = DeclarationManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

    }
}
