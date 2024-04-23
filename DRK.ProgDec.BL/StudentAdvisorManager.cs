﻿using DRK.ProgDec.PL;

namespace DRK.ProgDec.BL
{
    public class StudentAdvisorManager
    {
        public static void Insert(int studentId, int advisorId, bool rollback = false)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblStudentAdvisor tblStudentAdvisor = new tblStudentAdvisor();
                    tblStudentAdvisor.StudentId = studentId;
                    tblStudentAdvisor.AdvisorId = advisorId;
                    tblStudentAdvisor.Id = dc.tblStudentAdvisors.Any() ? dc.tblStudentAdvisors.Max(sa => sa.Id) + 1 : 1;

                    dc.tblStudentAdvisors.Add(tblStudentAdvisor);
                    dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Delete(int studentId, int advisorId, bool rollback = false)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblStudentAdvisor? tblStudentAdvisor = dc.tblStudentAdvisors
                                                            .FirstOrDefault(sa => sa.StudentId == studentId
                                                            && sa.AdvisorId == advisorId);
                    if (tblStudentAdvisor != null)
                    {
                        dc.tblStudentAdvisors.Remove(tblStudentAdvisor);
                        dc.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
