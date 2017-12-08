using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using FinalProject.DataClasses;
using FinalProject.Main;



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
        /// <returns>all items from inventory</returns>
        //public DataSet allInventoryItems()
        //{
        //    try
        //    {
        //        DataSet myDS;
        //        int iRet = 0;
        //        string sSQL = "SELECT ItemDesc FROM ItemDesc";
        //        myDS = db.ExecuteSQLStatement(sSQL, ref iRet);
        //        return myDS;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        //    }
        //}

        /// <summary>
        /// retrieves cost of specified item from db using ItemCode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns>cost of item</returns>
        public DataSet GetItemCost(string itemCode)
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
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum + " AND WHERE LineItemNum = " + lineItemNum ;
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
        /// <summary>
        /// method to add invoice to db
        /// </summary>
        /// <param name="totalCharge">user entered total charge</param>
        /// <param name="invoiceDate">user entered date of invoice</param>
        public void AddInvoice(double totalCharge, DateTime invoiceDate)
        {
            try
            {
                //DataSet myDS;
                int iRet = 0;
                string sSQL = "INSERT into Invoices (TotalCharge, InvoiceDate) VALUES (" + totalCharge + ", '" + invoiceDate.ToShortDateString() + "')";
                iRet = db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// method to delete invoice from db
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void DelInvoice(int invoiceNum)
        {
            try
            {
                string sqlDelInvoice = "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNum;
                string sqlDelItems = "DELETE LineItems.* FROM LineItems WHERE InvoiceNum = " + invoiceNum;

                db.ExecuteNonQuery(sqlDelItems);
                db.ExecuteNonQuery(sqlDelInvoice);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// method to load all invoices 
        /// </summary>
        /// <returns>array of invoices</returns>
        public InvoicesData[] LoadAllInvoices()
        {
            try
            {
                int iRetVal = 0;


                List<InvoicesData> InvoiceList = new List<InvoicesData>();

                clsDataAccess DataAccess = new clsDataAccess(); // new instance of clsDataAccess for opening connection

                DataSet data = DataAccess.ExecuteSQLStatement("SELECT * FROM Invoices", ref iRetVal); // SQL statement to load information from Flight table

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    InvoiceList.Add(new InvoicesData((int)row[0], (DateTime)row[1], (double)row[2]));
                }

                return InvoiceList.ToArray();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
            finally
            {
                //This code will always execute

            }
        }

        /// <summary>
        /// method to load all items for specific invoice from LineItems
        /// </summary>
        /// <param name="invoiceNum">invoice nubmer</param>
        /// <returns>array of items on invoice</returns>
        public DataSet LoadAllItemsFromInvoiceAsDataSet(int invoiceNum)
        {
            try
            {
                int iRetVal = 0;

                clsDataAccess DataAccess = new clsDataAccess();

                DataSet data = DataAccess.ExecuteSQLStatement("SELECT * FROM LineItems WHERE InvoiceNum = " + invoiceNum, ref iRetVal);

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
            finally
            {
                //This code will always execute

            }

        }

        /// <summary>
        /// method to load all items for specific invoice from LineItems
        /// </summary>
        /// <param name="invoiceNum">invoice nubmer</param>
        /// <returns>array of items on invoice</returns>
        public LineItemsData[] LoadAllItemsFromInvoice(int invoiceNum)
        {
            try
            {
                List<LineItemsData> InvoiceItemsList = new List<LineItemsData>();

                DataSet data = LoadAllItemsFromInvoiceAsDataSet(invoiceNum);

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    InvoiceItemsList.Add(new LineItemsData((int)row[0], (int)row[1], (string)row[2]));
                }

                return InvoiceItemsList.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
            finally
            {
                //This code will always execute

            }

        }

         public DataSet LoadAllInventoryItemsAsDataSet()
        {
            try
            {
                int iRetVal = 0;
                clsDataAccess DataAccess = new clsDataAccess();

                DataSet data = DataAccess.ExecuteSQLStatement ("SELECT * from ItemDesc", ref iRetVal);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
            finally
            {
                //This code will always execute

            }

        }

        /// <summary>
        /// method that loads all inventory items data into an array
        /// </summary>
        /// <returns>array of inventory items</returns>
        public ItemDescData[] LoadAllInventoryItems()
        {
            try
            {
               List<ItemDescData> InventoryItemsList = new List<ItemDescData>();

                DataSet data = LoadAllInventoryItemsAsDataSet();

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    InventoryItemsList.Add(new ItemDescData((string)row[0], (string)row[1], (Decimal) row[2]));
                }

                return InventoryItemsList.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
            finally
            {
                //This code will always execute

            }


        }
    }
}
