﻿using DRK.ProgDec.BL.Models;
using DRK.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.ProgDec.BL
{
    public static class DegreeTypeManager
    {
        public static int Insert(string description, ref int id, bool rollback = false)
        {
            try
            {
                DegreeType degreeType = new DegreeType
                {
                    Description = description

                };
                int results = Insert(degreeType, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = degreeType.ID;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblDegreeType entity = new tblDegreeType();
                    entity.Id = dc.tblDegreeTypes.Any() ? dc.tblDegreeTypes.Max(s => s.Id) + 1 : 1;
                    entity.Description = degreeType.Description;


                    // IMPORTANT - BACK FILL THE ID
                    degreeType.ID = entity.Id;

                    dc.tblDegreeTypes.Add(entity);
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

        public static int Update(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == degreeType.ID);

                    if (entity != null)
                    {
                        entity.Description = degreeType.Description;

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
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblDegreeTypes.Remove(entity);
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

        public static DegreeType LoadById(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new DegreeType
                        {
                            ID = entity.Id,
                            Description = entity.Description

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

        public static List<DegreeType> Load()
        {
            try
            {
                List<DegreeType> list = new List<DegreeType>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblDegreeTypes
                     select new
                     {
                         s.Id,
                         s.Description

                     })
                     .ToList()
                     .ForEach(degreeType => list.Add(new DegreeType
                     {
                         ID = degreeType.Id,
                         Description = degreeType.Description

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
