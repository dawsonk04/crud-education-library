namespace DRK.ProgDec.PL.Test
{
    [TestClass]
    public class utStudent
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
            Assert.AreEqual(5, dc.tblStudents.Count());



        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblStudent entity = new tblStudent();

            entity.FirstName = "hugo";
            entity.LastName = "morgan";
            entity.StudentID = "2352";
            entity.Id = -99;
            // add entity to DB
            dc.tblStudents.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblStudent - use the first one
            tblStudent entity = dc.tblStudents.FirstOrDefault();

            // Change property values
            entity.FirstName = "hugo";
            entity.LastName = "morgan";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblStudent where id = 4
            tblStudent entity = dc.tblStudents.Where(e => e.Id == 4).FirstOrDefault();

            dc.tblStudents.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }
    }
}
