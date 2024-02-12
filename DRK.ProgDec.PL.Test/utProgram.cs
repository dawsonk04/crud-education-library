namespace DRK.ProgDec.PL.Test
{
    [TestClass]
    public class utProgram
    {
        [TestMethod]
        public void LoadTest()
        {
            ProgDecEntities dc = new ProgDecEntities();
            Assert.AreEqual(16, dc.tblPrograms.Count());



        }

        [TestMethod]

        public void InsertTest()
        {
            var dc = new ProgDecEntities();

            // make entity
            tblProgram entity = new tblProgram();

            entity.DegreeTypeId = 2;
            entity.Description = "basket weaving";
            entity.Id = -99;
            // add entity to DB
            dc.tblPrograms.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }




    }
}