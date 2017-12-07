using FinalProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows;


//This is the DataAccess class provided for us that you said we could use.
//Credits: Shawn Cowder

/// <summary>
/// Class used to access the database.
/// </summary>
public class clsEditSQL 
{

    public void updateItems(string itemcode, string itemDesc, string cost)
    {
        /*
         * Update data in the ItemDesc table:
         * UPDATE ItemDesc SET ItemCode = 'BB', ItemDesc = 'itemDesc', Cost = 'cost' WHERE ItemCode='itemcode';
         */
    }

    public DataSet pullItemsTable()
    {
        List<clsItems> items = new List<clsItems>();
        DataSet ds = new DataSet("Items");
        items.Clear();

        try
        {
            clsDataAccess da = new clsDataAccess();


            String table = "SELECT * FROM ItemDesc";
            int recordCount = 0;
            ds = da.ExecuteSQLStatement(table, ref recordCount);
            for (int i = 0; i < recordCount; i++)
            {
                clsItems item = new clsItems();
                item.Item_Code = ds.Tables[0].Rows[i][0].ToString();
                item.Item_Desc = ds.Tables[0].Rows[i][1].ToString();
                item.Cost = Convert.ToDecimal(ds.Tables[0].Rows[i][2].ToString());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("PullItemsTable: " + ex.Message);
        }

        return ds;

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
            String checkIfExists = "SELECT * FROM LINEITEMS WHERE ItemCode = '" + itemcode + "'";
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