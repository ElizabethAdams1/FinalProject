using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;



namespace FinalProject.Main
{
    class clsMainSQL
    {
        /// <summary>
        /// main db object for this SQL class
        /// </summary>
        clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// Returns all the inventory Items in the Inventory table
        /// </summary>
        /// <returns></returns>
        public DataSet allInventoryItems()
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT ItemDesc FROM ItemDesc";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// retrieves cost of specified item from db using ItemCode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns>cost of item</returns>
        public DataSet itemCost(string itemCode)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT Cost FROM ItemDesc Where ItemCode = '" + itemCode + "'";
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
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public DataSet SelectInvoiceData(string invoiceNum)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = '" + invoiceNum + "'";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// deletes an item from LineItems table
        /// </summary>
        /// <param name="lineItemNum">the line of the invoice to be deleted</param>
        /// <param name="invoiceNum">The number of the invoice being referenced</param>
        /// <returns></returns>
        public DataSet deleteInvoiceItem(int lineItemNum, int invoiceNum)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = '" + invoiceNum + "' AND WHERE LineItemNum = '" + lineItemNum + "'";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// adds an atem to LineItem table
        /// </summary>
        /// <param name="invoiceNum">line number in invoice of item being added</param>
        /// <param name="itemCode">invoice number being added to</param>
        public DataSet addInvoiceItem(int invoiceNum, string itemCode)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "INSERT INTO LineItems (InvoiceNum, ItemCode) VALUES (" + invoiceNum + ", " + itemCode + ")";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
                return myDS;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void addInvoice(double totalCharge, DateTime invoiceDate)
        {
            try
            {
                DataSet myDS;
                int iRet = 0;
                string sSQL = "INSERT into Invoices (TotalCharge, InvoiceDate) VALUES (" + totalCharge + ", " + invoiceDate + ")";
                myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

    }
}
