namespace DRK.ProgDec.PL.Test
{
    [TestClass]
    public class utProgram
    {
        protected ProgDecEntities dc;
        protected IDbContextTransaction transaction;



        [TestInitialize]
        public void Initialize()
        {

            dc = new ProgDecEntities();
            transaction = dc.Database.BeginTransaction();

        }

        [TestCleanup]

        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(16, dc.tblPrograms.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


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

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblProgram - use the first one
            tblProgram entity = dc.tblPrograms.FirstOrDefault();

            // Change property values
            entity.Description = "new description";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblProgram where id = 4
            tblProgram entity = dc.tblPrograms.Where(e => e.Id == 4).FirstOrDefault();

            dc.tblPrograms.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]

        public void LoadByID()
        {
            tblProgram entity = dc.tblPrograms.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }

    }
}