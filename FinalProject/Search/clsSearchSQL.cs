using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Search
{
    class clsSearchSQL
    {

        static clsDataAccess db = new clsDataAccess();

        public static DataSet allInvoiceNumbers()
        {
            DataSet myDS;
            int iRet = 0;
            string sSQL = "SELECT InvoiceNum FROM Invoices";
            myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
            return myDS;
        }
    }
}
