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
        //Invoices invoices = new Invoices();

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



        //public DataSet LoadAllInventoryItemsAsDataSet()
        //{
        //    try
        //    {
        //        clsMainSQL sql = new clsMainSQL();
        //        return sql.LoadAllInventoryItemsAsDataSet();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        //    }

        //}



    }
}
