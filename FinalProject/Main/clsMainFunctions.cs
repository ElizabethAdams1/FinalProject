using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using FinalProject.DataClasses;
using System.Data;

namespace FinalProject.Main
{
    class clsMainFunctions
    {

        /// <summary>
        /// Method that calls delete query from clsMainSQL to delete invoice
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        public void DeleteInvoice(int invoiceNum)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                sql.DelInvoice(invoiceNum);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// method that calls sql statements in clsMainSQL to add an invoice to db
        /// </summary>
        /// <param name="totalCharge">total charge of invoice</param>
        /// <param name="invoiceDate">date of invoice</param>
        public void AddInvoice(double totalCharge, DateTime invoiceDate)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                sql.AddInvoice(totalCharge, invoiceDate);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// method that loads invoice items from db calling LoadAllItemsFromInvoice in clsMainSQL invoice number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>array of invoice information</returns>
        public LineItemsData[] LoadAllInvoiceItems(int invoiceNum)
        {
            try
            {
               clsMainSQL sql = new clsMainSQL();
               return sql.LoadAllItemsFromInvoice(invoiceNum);                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }

        /// <summary>
        /// method that loads invoice items from db calling LoadAllItemsFromInvoice in clsMainSQLinvoice number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>array of invoice information</returns>
        public DataSet LoadAllInvoiceItemsAsDataSet(int invoiceNum)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                return sql.LoadAllItemsFromInvoiceAsDataSet(invoiceNum);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }

        /// <summary>
        /// loads all inventory items 
        /// </summary>
        /// <returns>all inventory items</returns>
        public ItemDescData[] LoadAllInventoryItems()
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                return sql.LoadAllInventoryItems();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }

        /// <summary>
        /// loads all inventory items as a data set
        /// </summary>
        /// <returns>data set</returns>
        public DataSet LoadAllInventoryItemsAsDataSet()
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                return sql.LoadAllInventoryItemsAsDataSet();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// deletes invoice from db
        /// </summary>
        /// <param name="lineItemNum">line number of item</param>
        /// <param name="invoiceNum">invoice number</param>
        public void DeleteInvoiceItem(int lineItemNum, int invoiceNum)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                sql.DeleteInvoiceItem(lineItemNum, invoiceNum);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// creates array of invoice details
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <returns>invoice details</returns>
        public InvoiceDetailsData[] LoadInvoiceDetails(int invoiceNum)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                return sql.LoadInvoiceDetails(invoiceNum);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }

        /// <summary>
        /// data set for invoice details
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <returns>data set</returns>
        public DataSet LoadInvoiceDetailsAsDataSet(int invoiceNum)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                return sql.LoadAllItemsFromInvoiceAsDataSet(invoiceNum);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }

        /// <summary>
        /// updates the total of the invoice
        /// </summary>
        /// <param name="invoiceNum"i>invoice number</param>
        /// <param name="invoiceTotal">invoice total cost</param>
        /// <returnst>updated total cost</returns>
        public int UpdateInvoiceTotal(int invoiceNum, Decimal invoiceTotal)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                return sql.UpdateInvoiceTotal(invoiceNum, invoiceTotal);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// adds item to invoice
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <param name="itemCode">item code for item to be added</param>
        /// <returns></returns>
        public int AddInvoiceItem(int invoiceNum, string itemCode)
        {
            try
            {
                int lineItemNum = 0;
                clsMainSQL sql = new clsMainSQL();

                // Load all current invoice items to find the next line item number.
                LineItemsData[] data = sql.LoadAllItemsFromInvoice(invoiceNum);

                // Loop through all current invoice items to find next number.
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].LineItemNumber != (i + 1))
                    {
                        lineItemNum = i + 1;
                    }
                }
                if (lineItemNum == 0)
                {
                    lineItemNum = data.Length + 1;  
                }

                // Call the SQL to add the new invoice item.
                return sql.AddInvoiceItem(invoiceNum, lineItemNum, itemCode);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
