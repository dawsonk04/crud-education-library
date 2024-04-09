﻿using DRK.ProgDec.BL.Models;
using DRK.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace DRK.ProgDec.BL
{
    public static class StudentManager
    {
        public static int Insert(string firstName, string lastName, string studentId, ref int id, bool rollback = false)
        {
            try
            {
                Student student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    StudentId = studentId

                };
                int results = Insert(student, rollback);

                // IMPORTANT - BACKFILL THE REF
                id = student.ID;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Student student, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblStudent entity = new tblStudent();
                    entity.Id = dc.tblStudents.Any() ? dc.tblStudents.Max(s => s.Id) + 1 : 1;
                    entity.FirstName = student.FirstName;
                    entity.LastName = student.LastName;
                    entity.StudentId = student.StudentId;

                    // IMPORTANT - BACK FILL THE ID
                    student.ID = entity.Id;

                    dc.tblStudents.Add(entity);
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

        public static int Update(Student student, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // get the row we are trying to update
                    tblStudent entity = dc.tblStudents.FirstOrDefault(s => s.Id == student.ID);

                    if (entity != null)
                    {
                        entity.FirstName = student.FirstName;
                        entity.LastName = student.LastName;
                        entity.StudentId = student.StudentId;
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
                    tblStudent entity = dc.tblStudents.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblStudents.Remove(entity);
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

        public static Student LoadById(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblStudent entity = dc.tblStudents.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new Student
                        {
                            ID = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            StudentId = entity.StudentId
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

        public static List<Student> Load()
        {
            try
            {
                List<Student> list = new List<Student>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblStudents
                     select new
                     {
                         s.Id,
                         s.FirstName,
                         s.LastName,
                         s.StudentId
                     })
                     .ToList()
                     .ForEach(student => list.Add(new Student
                     {
                         ID = student.Id,
                         FirstName = student.FirstName,
                         LastName = student.LastName,
                         StudentId = student.StudentId
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
