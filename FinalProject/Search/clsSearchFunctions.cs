using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalProject.Search
{
    /// <summary>
    /// The functions used on the search form
    /// </summary>
    class clsSearchFunctions
    {
        clsSearchSQL mySQL = new clsSearchSQL();
        public void fillInvoiceDates(ComboBox cb)
        {
            DataSet myDS = mySQL.allInvoiceDates();
            for(int i = 0; i < myDS.Tables[0].Rows.Count; i++)
            {
                cb.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
            }
        }

        public void fillInvoiceTotalCharges(ComboBox cb)
        {
            DataSet myDS = mySQL.allInvoiceTotalChanges();
            for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
            {
                cb.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
            }
        }

        public void fillInvoiceNumbers(ComboBox cb)
        {
            DataSet myDS = mySQL.allInvoiceNumbers();
            for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
            {
                cb.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
            }
        }

        public void fillInvoices(DataGrid dg)
        {
            DataSet myDS = mySQL.returnAllInvoices();
            dg.DataContext = myDS.Tables[0].DefaultView;
        }

        public void filterByNum(string number, DataGrid dg)
        {
            DataSet myDS = mySQL.SelectInvoiceData(number);
            dg.DataContext = myDS.Tables[0].DefaultView;
            
        }

        public void filterByDate(string date, DataGrid dg)
        {
            DataSet myDS = mySQL.selectInvoiceWithDate(date);
            dg.DataContext = myDS.Tables[0].DefaultView;
        }

        public void filterByTotalCharges(string totCharges, DataGrid dg)
        {
            DataSet myDS = mySQL.selectInvoiceWithTotalCharges(totCharges);
            dg.DataContext = myDS.Tables[0].DefaultView;
        }
    }
}
