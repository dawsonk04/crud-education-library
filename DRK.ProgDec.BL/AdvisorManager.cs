using DRK.ProgDec.BL.Models;
using DRK.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.ProgDec.BL
{
    public static class AdvisorManager
    {
        public static int Insert(string name, ref int id, bool rollback = false)
        {
            try
            {
                Advisor advisor = new Advisor
                {
                    Name = name

                };
                int results = Insert(advisor, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = advisor.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Advisor advisor, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblAdvisor entity = new tblAdvisor();
                    entity.Id = dc.tblAdvisors.Any() ? dc.tblAdvisors.Max(s => s.Id) + 1 : 1;
                    entity.Name = advisor.Name;



                    // IMPORTANT - BACK FILL THE ID
                    advisor.Id = entity.Id;

                    dc.tblAdvisors.Add(entity);
                    results += dc.SaveChanges();


                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Update(Advisor advisor, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(s => s.Id == advisor.Id);

                    if (entity != null)
                    {
                        entity.Name = advisor.Name;

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
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblAdvisors.Remove(entity);
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

        public static Advisor LoadById(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new Advisor
                        {
                            Id = entity.Id,
                            Name = entity.Name,

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
        //editing or creating movie in DVDCentral
        public static List<Advisor> Load(int studentId)
        {
            try
            {
                List<Advisor> list = new List<Advisor>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblAdvisors
                     join sa in dc.tblStudentAdvisors on s.Id equals sa.AdvisorId
                     where sa.StudentId == studentId
                     select new
                     {
                         s.Id,
                         s.Name

                     })
                     .ToList()
                     .ForEach(advisor => list.Add(new Advisor
                     {
                         Id = advisor.Id,
                         Name = advisor.Name,

                     })); ;
                }

                return list;
            }

            catch (Exception)
            {

                throw;
            }
        }


        public static List<Advisor> Load()
        {
            try
            {
                List<Advisor> list = new List<Advisor>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblAdvisors
                     select new
                     {
                         s.Id,
                         s.Name

                     })
                     .ToList()
                     .ForEach(advisor => list.Add(new Advisor
                     {
                         Id = advisor.Id,
                         Name = advisor.Name,

                     })); ;
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
