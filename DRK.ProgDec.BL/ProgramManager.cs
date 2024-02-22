using DRK.ProgDec.BL.Models;
using DRK.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.ProgDec.BL
{
    public static class ProgramManager
    {
        public static int Insert(string description, int degreeTypeId, ref int id, bool rollback = false)
        {
            try
            {
                Program program = new Program
                {
                    Description = description,
                    DegreeTypeID = degreeTypeId

                };
                int results = Insert(program, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = program.ID;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Program program, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblProgram entity = new tblProgram();
                    entity.Id = dc.tblPrograms.Any() ? dc.tblPrograms.Max(s => s.Id) + 1 : 1;
                    entity.Description = program.Description;
                    entity.DegreeTypeId = program.DegreeTypeID;


                    // IMPORTANT - BACK FILL THE ID
                    program.ID = entity.Id;

                    dc.tblPrograms.Add(entity);
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

        public static int Update(Program program, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblProgram entity = dc.tblPrograms.FirstOrDefault(s => s.Id == program.ID);

                    if (entity != null)
                    {
                        entity.Description = program.Description;
                        entity.DegreeTypeId = program.DegreeTypeID;

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
                    tblProgram entity = dc.tblPrograms.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblPrograms.Remove(entity);
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

        public static Program LoadById(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblProgram entity = dc.tblPrograms.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new Program
                        {
                            ID = entity.Id,
                            Description = entity.Description,
                            DegreeTypeID = entity.DegreeTypeId

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

        public static List<Program> Load()
        {
            try
            {
                List<Program> list = new List<Program>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblPrograms
                     select new
                     {
                         s.Id,
                         s.Description,
                         s.DegreeTypeId
                     })
                     .ToList()
                     .ForEach(program => list.Add(new Program
                     {
                         ID = program.Id,
                         Description = program.Description,
                         DegreeTypeID = program.DegreeTypeId

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
