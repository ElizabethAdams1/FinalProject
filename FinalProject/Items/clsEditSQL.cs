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
/// Class used to access the database.
/// </summary>
public class clsEditSQL 
{

    public bool updateItems(string itemcode, string itemdesc, string cost)
    {
        bool result = false;
        decimal decCost = decimal.Parse(cost, System.Globalization.NumberStyles.Currency);
        int iRet = 0;
        //*Update data in the ItemDesc table:
        string saveItem = "UPDATE ItemDesc SET ItemDesc = '" + itemdesc + "', Cost = " + decCost + " WHERE ItemCode = '" + itemcode + "'";

        try
        {
            clsDataAccess da = new clsDataAccess();
            iRet = da.ExecuteNonQuery(saveItem);
            if(iRet == 0)
            {
                string insertItem = "INSERT INTO ItemDesc (Itemcode, ItemDesc, Cost) Values ('"+itemcode+"','"+itemdesc+"',"+decCost+")";
                iRet = da.ExecuteNonQuery(insertItem);
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
        catch
        {

        }

        return result;
    }

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
            MessageBox.Show("PullItemsTable: " + ex.Message);
        }

        return items;

    }

    public bool DeleteItem(string itemcode)
    {
        bool deleted = false;
        DataSet ds = new DataSet();
        //retrieve a random number for seating
        try
        {
            //initializes clsDataAccess class
            clsDataAccess da = new clsDataAccess();
            //check to see if seat number is already used
            String checkIfExists = "SELECT * FROM LineItems WHERE ItemCode = '" + itemcode + "'";
            String deleteItem = "DELETE FROM ItemDesc WHERE ItemCode = '" + itemcode + "'";
            int ret = 0;
            int rowsAffected = 0;
            string invoices = string.Empty;
            //SQL executes checkIfExists and check the number of records 
            ds = da.ExecuteSQLStatement(checkIfExists, ref ret);
            //if ret equals 0 then the value available for assignment for new passenger
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
        catch (Exception e)
        {
            //Exception message
            MessageBox.Show("checkAvailableSeat" + " " + e.Message);
        }

        return deleted;
    }
}