using FinalProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows;
using FinalProject.DataClasses;

/// <summary>
/// Quinn Anderson
/// CS3280
/// This class was created to hold all the 
/// business logic and to interface with 
/// the database.
/// </summary>
public class clsEditSQL 
{
    /// <summary>
    /// This method updates the "ItemDesc" table in the database
    /// It attempts the UPDATE however if the element does not already
    /// exist it will pass to an INSERT command to insert the new item.
    /// It returns a boolean value to let the UI side know if it was successfull.
    /// </summary>
    /// <param name="itemcode"></param>
    /// <param name="itemdesc"></param>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool updateItems(string itemcode, string itemdesc, string cost)
    {
        //holds the boolean value to pass to the UI based on success for failure
        bool result = false;
        //converts the string cost into a decimal value that the database would accept
        decimal decCost = decimal.Parse(cost, System.Globalization.NumberStyles.Currency);
        //holds the number of records effected by the UPDATE or INSERT as the case may be.
        int iRet = 0;
        //Update string
        string saveItem = "UPDATE ItemDesc SET ItemDesc = '" + itemdesc + "', Cost = " + decCost + " WHERE ItemCode = '" + itemcode + "'";

        try
        {
            //Initializes the Data Access class
            clsDataAccess da = new clsDataAccess();
            //Runs the sql command / returns the number rows effected
            iRet = da.ExecuteNonQuery(saveItem);
            //if the number of rows is 0 then it passes to the INSERT statement otherwise
            //the update was successful and result is set to true
            if(iRet == 0)
            {
                string insertItem = "INSERT INTO ItemDesc (Itemcode, ItemDesc, Cost) Values ('"+itemcode+"','"+itemdesc+"',"+decCost+")";
                iRet = da.ExecuteNonQuery(insertItem);
                //if iRet is 1 then the INSERT command was successful result is set to true
                if(iRet != 0)
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
        }

        return result;
    }
    /// <summary>
    /// This method pulls the data from the ItemDesc table
    /// Creates a List of ItemDescData objects
    /// The list is then passed back to the UI to populate the 
    /// datagrid.
    /// </summary>
    /// <returns></returns>
    public IList<ItemDescData> pullItemsTable()
    {
        List<ItemDescData> items = new List<ItemDescData>();
        DataSet ds;
        items.Clear();

        try
        {
            clsDataAccess da = new clsDataAccess();
            String table = "SELECT * FROM ItemDesc";
            int recordCount = 0;
            ds = da.ExecuteSQLStatement(table, ref recordCount);
            for (int i = 0; i < recordCount; i++)
            {
                ItemDescData item = new ItemDescData();
                item.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                item.ItemDesc = ds.Tables[0].Rows[i][1].ToString();
                item.Cost = Convert.ToDecimal(ds.Tables[0].Rows[i][2].ToString());
                items.Add(item);
            }
        }
        catch (Exception ex)
        {
            HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
        }

        return items;

    }

    /// <summary>
    /// This method deletes the selected value from the datagrid
    /// It is checked against the LineItems table to ensure it is not
    /// part of any existing invoices, if it is then the user is shown a
    /// message box letting them know which invoices.
    /// </summary>
    /// <param name="itemcode"></param>
    /// <returns></returns>
    public bool DeleteItem(string itemcode)
    {
        bool deleted = false;
        DataSet ds = new DataSet();
        //retrieve a random number for seating
        try
        {
            //initializes clsDataAccess class
            clsDataAccess da = new clsDataAccess();
            //check to see if item is part of an existing invoice
            String checkIfExists = "SELECT * FROM LineItems WHERE ItemCode = '" + itemcode + "'";
            String deleteItem = "DELETE FROM ItemDesc WHERE ItemCode = '" + itemcode + "'";
            int ret = 0;
            int rowsAffected = 0;
            string invoices = string.Empty;
            //SQL executes checkIfExists and check the number of records 
            ds = da.ExecuteSQLStatement(checkIfExists, ref ret);
            //if ret equals 0 then the the item is part of an invoice(s)
            if (ret != 0)
            {
                for (int i = 0; i < ret; i++)
                {
                    invoices = invoices + ds.Tables[0].Rows[i][0].ToString() + ", ";
                }
                MessageBox.Show("Item '" + itemcode + "' was found in the following invoices " + invoices);
            }
            else
            {
                rowsAffected = da.ExecuteNonQuery(deleteItem);
            }
            if (rowsAffected != 0)
            {
                MessageBox.Show("Item has been deleted");
                deleted = true;
            }
        }
        catch (Exception ex)
        {
            HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
        }

        return deleted;
    }

    /// <summary>
    /// Handles exceptions and messages
    /// </summary>
    /// <param name="sClass"></param>
    /// <param name="sMethod"></param>
    /// <param name="sMessage"></param>
    private void HandleError(string sClass, string sMethod, string sMessage)
    {
        try
        {
            MessageBox.Show(sClass + "." + sMethod + "->" + sMessage);
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error has occured: " + ex.Message);
        }
    }
}