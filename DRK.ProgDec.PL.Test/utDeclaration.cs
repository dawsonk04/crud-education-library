namespace DRK.ProgDec.PL.Test
{
    [TestClass]
    public class utDeclaration
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
            Assert.AreEqual(4, dc.tblDeclarations.Count());



        }

        [TestMethod]

        public void LoadAllTest()
        {
            var declaration = (from d in dc.tblDeclarations
                               join s in dc.tblStudents on d.StudentId equals s.Id
                               join p in dc.tblPrograms on d.ProgramId equals p.Id
                               join dt in dc.tblDegreeTypes on p.DegreeTypeId equals dt.Id
                               select new
                               {
                                   d.Id,
                                   StudentName = s.FirstName + " " + s.LastName,
                                   ProgramName = p.Description,
                                   DegreeTypeName = dt.Description

                               }).ToList();
            Assert.AreEqual(4, declaration.Count);
        }

        [TestMethod]

        public void InsertTest()
        {


            // make entity
            tblDeclaration entity = new tblDeclaration();

            entity.ChangeDate = DateTime.Now;
            entity.ProgramId = 12;
            entity.StudentId = 15;
            entity.Id = -99;
            // add entity to DB
            dc.tblDeclarations.Add(entity);

            // commit changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            // SELECT * FROM tblDeclaration - use the first one
            tblDeclaration entity = dc.tblDeclarations.FirstOrDefault();

            // Change property values
            entity.ChangeDate = DateTime.Now;
            entity.ProgramId = 12;
            entity.StudentId = 15;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]

        public void DeleteTest()
        {
            // SELECT * FROM tblDeclaration where id = 4
            tblDeclaration entity = dc.tblDeclarations.Where(e => e.Id == 4).FirstOrDefault();

            dc.tblDeclarations.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]

        public void LoadByID()
        {
            tblDeclaration entity = dc.tblDeclarations.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }

    }
}
