using DRK.ProgDec.BL.Models;
using DRK.ProgDec.PL;

namespace DRK.ProgDec.BL
{
    public static class DegreeTypeManager
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

        public static DegreeType LoadById(int id)
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

        public static List<DegreeType> Load()
        {
            try
            {
                List<DegreeType> list = new List<DegreeType>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from dt in dc.tblDegreeTypes
                     select new
                     {
                         dt.Id,
                         dt.Description
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
