using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDAL
    {
        private static IDAL iDal = null;
        public static IDAL GetDAL()
        {

            if (iDal == null)
                iDal = new DALimp();
            return iDal;

        }
    }

}
