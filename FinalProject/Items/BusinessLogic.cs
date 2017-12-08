using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

/// <summary>
/// Business Logic class
/// </summary>
namespace FinalProject
{
    class BusinessLogic
    {
        /// <summary>
        /// Run from edit/new to update SQL database
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="Item"></param>
        /// <param name="Quantity"></param>
        public void updateSQL (string itemcode, string itemDesc, string cost)
        {
            /*
             * Update data in the ItemDesc table:
             * UPDATE ItemDesc SET ItemCode = 'BB', ItemDesc = 'itemDesc', Cost = 'cost' WHERE ItemCode='itemcode';
             */
        }

        public DataSet pullTable(string table)
        {
            clsEditSQL es = new clsEditSQL();
            DataSet ds = new DataSet();
            int recordCount = 0;
            ds = es.ExecuteSQLStatement(table, ref recordCount);
            return ds;
        }

        public void DeleteItem(string itemcode)
        {
            /*
             * SQL command to delete selected item.
             * Check to see if item is part of an existing invoice
             * if not then delete from database table
             */

            /*
             * Delete data from the ItemDesc table:
             * DELETE FROM ItemDesc WHERE ItemCode = 'itemcode';
             */
        }
    }
}
