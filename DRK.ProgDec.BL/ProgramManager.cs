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
                    var entity = (from s in dc.tblPrograms
                                  join dt in dc.tblDegreeTypes on s.DegreeTypeId equals dt.Id
                                  where s.Id == id
                                  select new
                                  {
                                      s.Id,
                                      s.Description,
                                      s.DegreeTypeId,
                                      DegreeTypeName = dt.Description
                                  })
                                    .FirstOrDefault();

                    if (entity != null)
                    {
                        return new Program
                        {
                            ID = entity.Id,
                            Description = entity.Description,
                            DegreeTypeID = entity.DegreeTypeId,
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

        public static List<Program> Load()
        {
            try
            {
                List<Program> list = new List<Program>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblPrograms
                     join dt in dc.tblDegreeTypes on s.DegreeTypeId equals dt.Id
                     select new
                     {
                         s.Id,
                         s.Description,
                         s.DegreeTypeId,
                         DegreeTypeName = dt.Description
                     })
                     .ToList()
                     .ForEach(program => list.Add(new Program
                     {
                         ID = program.Id,
                         Description = program.Description,
                         DegreeTypeID = program.DegreeTypeId,
                         DegreeTypeName = program.DegreeTypeName
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
