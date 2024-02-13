namespace DRK.ProgDec.PL.Test
{
    [TestClass]
    public class utDegreeType
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
            Assert.AreEqual(3, dc.tblDegreeTypes.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblDegreeType entity = new tblDegreeType();

            entity.Description = "description";
            entity.Id = -99;
            // add entity to DB
            dc.tblDegreeTypes.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblDegreeType - use the first one
            tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault();

            // Change property values
            entity.Description = "description";


            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblDegreeType where id = 4
            tblDegreeType entity = dc.tblDegreeTypes.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblDegreeTypes.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}
