using DRK.ProgDec.BL.Models;
using DRK.ProgDec.PL;

namespace DRK.ProgDec.BL
{
    public static class DeclarationManager
    {
        public static int Insert()
        {
            try
            {
                return 0;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Update()
        {
            try
            {
                return 0;
            }
            catch (Exception)
            {

                throw;

            }

        }

        public static int Delete()
        {
            try
            {
                return 0;
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
                return null!;
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
                    (from d in dc.tblDeclarations
                     select new
                     {
                         d.Id,
                         d.ProgramId,
                         d.StudentId,
                         d.ChangeDate
                     })
                     .ToList()
                     .ForEach(declaration => list.Add(new Declaration
                     {
                         ID = declaration.Id,
                         ProgramID = declaration.ProgramId,
                         StudentID = declaration.StudentId,
                         ChangeDate = declaration.ChangeDate
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
