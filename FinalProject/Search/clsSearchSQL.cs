using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// main db object for this SQL class
        /// </summary>
        clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// Returns all the invoice numbers in the invoice table
        /// </summary>
        /// <returns></returns>
        public DataSet allInvoiceNumbers()
        {
            try { 
            DataSet myDS;
            int iRet = 0;
            string sSQL = "SELECT InvoiceNum FROM Invoices";
            myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
            return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns all the invoice dates in the table
        /// </summary>
        /// <returns></returns>
        public DataSet allInvoiceDates()
        {
            try { 
            DataSet myDS;
            int iRet = 0;
            string sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices";
            myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
            return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all the invoice total changes in the invoice table
        /// </summary>
        /// <returns></returns>
        public DataSet allInvoiceTotalChanges()
        {
            try { 
            DataSet myDS;
            int iRet = 0;
            string sSQL = "SELECT DISTINCT TotalCharge FROM Invoices ORDER BY TotalCharge ASC";
            myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
            return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Returns all data for one invoice id, given the invoice id
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <returns></returns>
        public DataSet SelectInvoiceData(string sInvoiceID)
        {
            try { 
            DataSet myDS;
            int iRet = 0;
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;
            myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
            return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public DataSet selectInvoiceWithDate(string date)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "#";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public DataSet selectInvoiceWithTotalCharges(string totCharge)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT * FROM Invoices WHERE TotalCharge =" +totCharge;
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public DataSet returnAllInvoices()
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT * FROM Invoices";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
