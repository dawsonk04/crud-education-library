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
                    tblDeclaration entity = dc.tblDeclarations.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new Declaration
                        {
                            ID = entity.Id,
                            ProgramID = entity.ProgramId,
                            StudentID = entity.StudentId,
                            ChangeDate = DateTime.Now
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

        public static List<Declaration> Load()
        {
            try
            {
                List<Declaration> list = new List<Declaration>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblDeclarations
                     select new
                     {
                         s.Id,
                         s.ProgramId,
                         s.StudentId,
                         s.ChangeDate
                     })
                     .ToList()
                     .ForEach(declaration => list.Add(new Declaration
                     {
                         ID = declaration.Id,
                         ProgramID = declaration.ProgramId,
                         StudentID = declaration.StudentId,
                         ChangeDate = DateTime.Now
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
