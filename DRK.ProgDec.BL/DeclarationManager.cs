using DRK.ProgDec.BL.Models;
using DRK.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.ProgDec.BL
{
    public static class DeclarationManager
    {
        public static int Insert(int programId, int studentId, ref int id, bool rollback = false)
        {
            try
            {
                Declaration declaration = new Declaration
                {
                    ProgramID = programId,
                    StudentID = studentId,
                    ChangeDate = DateTime.Now

                };
                int results = Insert(declaration, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = declaration.ID;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Declaration declaration, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblDeclaration entity = new tblDeclaration();
                    entity.Id = dc.tblDeclarations.Any() ? dc.tblDeclarations.Max(s => s.Id) + 1 : 1;
                    entity.ProgramId = declaration.ProgramID;
                    entity.StudentId = declaration.StudentID;
                    entity.ChangeDate = DateTime.Now;


                    // IMPORTANT - BACK FILL THE ID
                    declaration.ID = entity.Id;

                    dc.tblDeclarations.Add(entity);
                    results = dc.SaveChanges();


                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Update(Declaration declaration, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblDeclaration entity = dc.tblDeclarations.FirstOrDefault(s => s.Id == declaration.ID);

                    if (entity != null)
                    {
                        entity.ProgramId = declaration.ProgramID;
                        entity.StudentId = declaration.StudentID;
                        entity.ChangeDate = DateTime.Now;
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;

            }
            catch (Exception)
            {

                throw;

            }

        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblDeclaration entity = dc.tblDeclarations.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblDeclarations.Remove(entity);
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;

            }
            catch (Exception)
            {

                throw;

            }

        }

        public static Declaration LoadById(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    var entity = (from d in dc.tblDeclarations
                                  join s in dc.tblStudents on d.StudentId equals s.Id
                                  join p in dc.tblPrograms on d.ProgramId equals p.Id
                                  join dt in dc.tblDegreeTypes on p.DegreeTypeId equals dt.Id
                                  where d.Id == id
                                  select new
                                  {
                                      d.Id,
                                      StudentName = s.FirstName + " " + s.LastName,
                                      ProgramName = p.Description,
                                      DegreeTypeName = dt.Description,
                                      d.ProgramId,
                                      d.StudentId,
                                      d.ChangeDate
                                  })
                                 .FirstOrDefault();

                    if (entity != null)
                    {
                        return new Declaration
                        {
                            ID = entity.Id,
                            ProgramID = entity.ProgramId,
                            StudentID = entity.StudentId,
                            ChangeDate = DateTime.Now,
                            StudentName = entity.StudentName,
                            ProgramName = entity.ProgramName,
                            DegreeTypeName = entity.DegreeTypeName
                        };
                    }
                    else
                    {
                        throw new Exception();
                    }


                }


            }

            catch (Exception)
            {

                throw;
            }
        }

        public static List<Declaration> Load(int? programId = null)
        {
            try
            {
                List<Declaration> list = new List<Declaration>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from d in dc.tblDeclarations
                     join s in dc.tblStudents on d.StudentId equals s.Id
                     join p in dc.tblPrograms on d.ProgramId equals p.Id
                     join dt in dc.tblDegreeTypes on p.DegreeTypeId equals dt.Id
                     where d.ProgramId == programId || programId == null
                     select new
                     {
                         d.Id,
                         StudentName = s.FirstName + " " + s.LastName,
                         ProgramName = p.Description,
                         DegreeTypeName = dt.Description,

                         d.ProgramId,
                         d.StudentId,
                         d.ChangeDate

                     })
                     // Need this for checkpoint whatever
                     .Distinct()
                     .ToList()
                     .ForEach(declaration => list.Add(new Declaration
                     {
                         ID = declaration.Id,
                         ProgramID = declaration.ProgramId,
                         StudentID = declaration.StudentId,
                         ChangeDate = DateTime.Now,
                         StudentName = declaration.StudentName,
                         ProgramName = declaration.ProgramName,
                         DegreeTypeName = declaration.DegreeTypeName,
                     }));
                }

                return list;
            }

            catch (Exception)
            {

                throw;
            }
        }







    }
}
