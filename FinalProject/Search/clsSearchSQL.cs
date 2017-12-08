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
        /// <summary>
        /// Select invoice data filtered by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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
        /// <summary>
        /// select invoice data filtered by total charges
        /// </summary>
        /// <param name="totCharge"></param>
        /// <returns></returns>
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
        /// <summary>
        /// select invoice data filtered by num and charge
        /// </summary>
        /// <param name="num"></param>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet filterByNumCharge(string num, string charge)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT * FROM Invoices WHERE TotalCharge =" + charge + " AND InvoiceNum = " + num;
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice data filtered by date and charge
        /// </summary>
        /// <param name="date"></param>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet filterByDateCharge(string date, string charge)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT * FROM Invoices WHERE TotalCharge =" + charge + " AND InvoiceDate = #" + date + "#";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice data filtered by date and num
        /// </summary>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataSet filterByDateNum(string date, string num)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum =" + num + " AND InvoiceDate = #" + date + "#";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice data filtered by date num charge
        /// </summary>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet filterByDateNumCharge(string date, string num, string charge)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum =" + num + " AND InvoiceDate = #" + date + "#" + " AND TotalCharge =" + charge;
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice date filtered by num
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataSet dateFilteredByNum(string num)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE InvoiceNum =" + num;
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice charge filtered by num
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataSet chargeFilteredByNum(string num)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT TotalCharge FROM Invoices WHERE InvoiceNum =" + num;
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice charge filtered by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet chargeFilteredByDate(string date)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT TotalCharge FROM Invoices WHERE InvoiceDate = #" + date + "#";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice num filtered by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet numFilteredByDate(string date)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT InvoiceNum FROM Invoices WHERE InvoiceDate = #" + date + "#";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice date filtered by charge
        /// </summary>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet dateFilteredByCharge(string charge)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE TotalCharge =" + charge;
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice num filtered by charge
        /// </summary>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet numFilteredByCharge(string charge)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT InvoiceNum FROM Invoices WHERE TotalCharge =" + charge;
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select incoive date filtered by charge and num
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataSet dateFilteredByChargeNum(string charge, string num)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE TotalCharge =" + charge + " AND InvoiceNum = " + num;
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice num filtered by charge and date
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet numFilteredByChargeDate(string charge, string date)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT InvoiceNum FROM Invoices WHERE TotalCharge =" + charge + " AND InvoiceDate = #" + date + "#";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select invoice charge filtered by num and date
        /// </summary>
        /// <param name="num"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet chargeFilteredByNumDate(string num, string date)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT TotalCharge FROM Invoices WHERE InvoiceNum =" + num + " AND InvoiceDate = #" + date + "#";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// select all invoices with no filter
        /// </summary>
        /// <returns></returns>
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
