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
        /// retrieves cost of specified item from db using ItemCode
        /// </summary>
        /// <param name="itemCode">item code</param>
        /// <returns>data set for cost of item</returns>
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
        /// <returns>data set for invoices</returns>
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
        public void DeleteInvoiceItem(int lineItemNum, int invoiceNum)
        {
            try
            {
                int iRet = 0;
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum + " AND LineItemNum = " + lineItemNum ;
                iRet = db.ExecuteNonQuery(sSQL);
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
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode">invoice number being added to</param>
        public int AddInvoiceItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            try
            {
                int iRet = 0;
                string sSQL = "INSERT INTO LineItems (InvoiceNum, lineItemNum, ItemCode) VALUES (" + invoiceNum + ", " + lineItemNum + ", '" + itemCode + "')";
                iRet = db.ExecuteNonQuery(sSQL);
                return iRet;
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
        /// <param name="invoiceNum">invoice number to delete from dbparam>
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
        /// method to load invoice details as a data set
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>data set</returns>
        public DataSet LoadInvoiceDetailsAsDataSet(int invoiceNum)
        {
            try
            {
                int iRetVal = 0;
                clsDataAccess DataAccess = new clsDataAccess();

                DataSet data = DataAccess.ExecuteSQLStatement("SELECT Invoices.InvoiceNum, ItemDesc.ItemDesc, LineItems.LineItemNum, ItemDesc.Cost FROM ((Invoices INNER JOIN LineItems ON Invoices.InvoiceNum = LineItems.InvoiceNum) INNER JOIN ItemDesc ON LineItems.ItemCode = ItemDesc.ItemCode) WHERE Invoices.InvoiceNum = " + invoiceNum + ";", ref iRetVal);

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
        /// method to load all invoice details into an array 
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <returns>array</returns>
        public InvoiceDetailsData[] LoadInvoiceDetails(int invoiceNum)
        {
            try
            {
                List<InvoiceDetailsData> InvoiceDetailsList = new List<InvoiceDetailsData>();

                DataSet data = LoadInvoiceDetailsAsDataSet(invoiceNum);

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    InvoiceDetailsList.Add(new InvoiceDetailsData((int)row[0], (string)row[1], (int)row[2], (Decimal)row[3]));
                }

                return InvoiceDetailsList.ToArray();
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
        /// data set of invoice information so that costs can be totaled. Tables are joined
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <returns>data set</returns>
        public DataSet TotalInvoiceSumAsDataSet(int invoiceNum)
        {
            try
            {
                int iRetVal = 0;
                clsDataAccess DataAccess = new clsDataAccess();

                DataSet data = DataAccess.ExecuteSQLStatement("SELECT Invoices.InvoiceNum, ItemDesc.ItemDesc, LineItems.LineItemNum, ItemDesc.Cost FROM ((Invoices INNER JOIN LineItems ON Invoices.InvoiceNum = LineItems.InvoiceNum) INNER JOIN ItemDesc ON LineItems.ItemCode = ItemDesc.ItemCode) WHERE Invoices.InvoiceNum = " + invoiceNum + ";", ref iRetVal);

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
        /// <summary>
        /// data set of all intventory items available 
        /// </summary>
        /// <returns>data set</returns>
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

        /// <summary>
        /// used to update the invoice total when items are added or deleted from it
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <param name="invoiceTotal">invoice total</param>
        /// <returns>iRetVal</returns>
        public int UpdateInvoiceTotal(int invoiceNum, Decimal invoiceTotal)
        {
            try
            {
                int iRetVal = 0;
                clsDataAccess DataAccess = new clsDataAccess();

                iRetVal = DataAccess.ExecuteNonQuery("UPDATE Invoices SET TotalCharge = " + invoiceTotal + " WHERE InvoiceNum = " + invoiceNum + ";");
                return iRetVal;
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
