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

        public void filterData(DataGrid dg, ComboBox cbNum, ComboBox cbDate, ComboBox cbCharge)
        {
            DataSet myDS;
            if (cbCharge.SelectedIndex == -1 && cbDate.SelectedIndex == -1 && cbNum.SelectedIndex != -1)
            {
                myDS = mySQL.SelectInvoiceData(cbNum.SelectedValue.ToString());
                dg.DataContext = myDS.Tables[0].DefaultView;
            }
            else if (cbCharge.SelectedIndex == -1 && cbNum.SelectedIndex == -1 && cbDate.SelectedIndex != -1)
            {
                myDS = mySQL.selectInvoiceWithDate(cbDate.SelectedValue.ToString());
                dg.DataContext = myDS.Tables[0].DefaultView;
            }
            else if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex == -1 && cbCharge.SelectedIndex != -1)
            {
                myDS = mySQL.selectInvoiceWithTotalCharges(cbCharge.SelectedValue.ToString());
                dg.DataContext = myDS.Tables[0].DefaultView;
            }
            else if (cbCharge.SelectedIndex != -1 && cbDate.SelectedIndex == -1 && cbNum.SelectedIndex != -1)
            {
                myDS = mySQL.filterByNumCharge(cbNum.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                dg.DataContext = myDS.Tables[0].DefaultView;
            }
            else if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex != -1)
            {
                myDS = mySQL.filterByDateCharge(cbDate.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                dg.DataContext = myDS.Tables[0].DefaultView;
            }
            else if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex != -1)
            {
                myDS = mySQL.filterByDateNum(cbDate.SelectedValue.ToString(), cbNum.SelectedValue.ToString());
                dg.DataContext = myDS.Tables[0].DefaultView;
            }
            else if (cbNum.SelectedIndex != -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex != -1)
            {
                myDS = mySQL.filterByDateNumCharge(cbDate.SelectedValue.ToString(), cbNum.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                dg.DataContext = myDS.Tables[0].DefaultView;
            }
        }

        //public void filterByNum(string number, DataGrid dg, ComboBox cbDate, ComboBox cbCharge)
        //{
        //    DataSet myDS;
        //    if (cbCharge.SelectedIndex == -1 && cbDate.SelectedIndex == -1)
        //    {
        //        myDS = mySQL.SelectInvoiceData(number);
        //        dg.DataContext = myDS.Tables[0].DefaultView;
        //    }
            
            
        //}

        //public void filterByDate(string date, DataGrid dg, ComboBox cbNum, ComboBox cbCharge)
        //{
        //    if (cbCharge.SelectedIndex == -1 && cbNum.SelectedIndex == -1)
        //    {
        //        DataSet myDS = mySQL.selectInvoiceWithDate(date);
        //        dg.DataContext = myDS.Tables[0].DefaultView;
        //    }
        //}

        //public void filterByTotalCharges(string totCharges, DataGrid dg, ComboBox cbNum, ComboBox cbDate)
        //{
        //    if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex == -1)
        //    {
        //        DataSet myDS = mySQL.selectInvoiceWithTotalCharges(totCharges);
        //        dg.DataContext = myDS.Tables[0].DefaultView;
        //    }

        //}

        public string findInvoiceNumber(DataGrid dg, ComboBox cbNum, ComboBox cbCharge, ComboBox cbDate)
        {
            int test = dg.SelectedIndex;
            string value = "-1";
            DataSet myDS;
            if (cbCharge.SelectedIndex == -1 && cbDate.SelectedIndex == -1 && cbNum.SelectedIndex == -1)
            {
                myDS = mySQL.allInvoiceNumbers();

                value = myDS.Tables[0].Rows[test][0].ToString();
               
            }
            else if (cbCharge.SelectedIndex == -1 && cbDate.SelectedIndex == -1 && cbNum.SelectedIndex != -1)
            {
                myDS = mySQL.SelectInvoiceData(cbNum.SelectedValue.ToString());
                value = myDS.Tables[0].Rows[test][0].ToString();
            }
            else if (cbCharge.SelectedIndex == -1 && cbNum.SelectedIndex == -1 && cbDate.SelectedIndex != -1)
            {
                myDS = mySQL.selectInvoiceWithDate(cbDate.SelectedValue.ToString());
                value = myDS.Tables[0].Rows[test][0].ToString();
            }
            else if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex == -1 && cbCharge.SelectedIndex != -1)
            {
                myDS = mySQL.selectInvoiceWithTotalCharges(cbCharge.SelectedValue.ToString());
                value = myDS.Tables[0].Rows[test][0].ToString();
            }
            else if (cbCharge.SelectedIndex != -1 && cbDate.SelectedIndex == -1 && cbNum.SelectedIndex != -1)
            {
                myDS = mySQL.filterByNumCharge(cbNum.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                value = myDS.Tables[0].Rows[test][0].ToString();
            }
            else if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex != -1)
            {
                myDS = mySQL.filterByDateCharge(cbDate.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                value = myDS.Tables[0].Rows[test][0].ToString();
            }
            else if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex != -1)
            {
                myDS = mySQL.filterByDateNum(cbDate.SelectedValue.ToString(), cbNum.SelectedValue.ToString());
                value = myDS.Tables[0].Rows[test][0].ToString();
            }
            else if (cbNum.SelectedIndex != -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex != -1)
            {
                myDS = mySQL.filterByDateNumCharge(cbDate.SelectedValue.ToString(), cbNum.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                value = myDS.Tables[0].Rows[test][0].ToString();
            }
            return value;
        }
    }
}
