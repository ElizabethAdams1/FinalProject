using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
        /// <summary>
        /// mySQL object
        /// </summary>
        clsSearchSQL mySQL = new clsSearchSQL();
        /// <summary>
        /// fill cbDate box
        /// </summary>
        /// <param name="cb"></param>
        public void fillInvoiceDates(ComboBox cb)
        {
            try
            {
                int iCount = cb.Items.Count;
                while (iCount > 0)
                {
                    cb.Items.RemoveAt(0);
                    iCount--;
                }
                DataSet myDS = mySQL.allInvoiceDates();
                for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                {
                    cb.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// fill tot charges box
        /// </summary>
        /// <param name="cb"></param>
        public void fillInvoiceTotalCharges(ComboBox cb)
        {
            try
            {
                int iCount = cb.Items.Count;
                while (iCount > 0)
                {
                    cb.Items.RemoveAt(0);
                    iCount--;
                }
                DataSet myDS = mySQL.allInvoiceTotalChanges();
                for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                {
                    cb.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// fill invoice num box
        /// </summary>
        /// <param name="cb"></param>
        public void fillInvoiceNumbers(ComboBox cb)
        {
            try
            {
                int iCount = cb.Items.Count;
                while (iCount > 0)
                {
                    cb.Items.RemoveAt(0);
                    iCount--;
                }
                DataSet myDS = mySQL.allInvoiceNumbers();
                for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                {
                    cb.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// fill original datagrid
        /// </summary>
        /// <param name="dg"></param>
        public void fillInvoices(DataGrid dg)
        {
            try
            {
                DataSet myDS = mySQL.returnAllInvoices();
                dg.DataContext = myDS.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// filter all data based on combo boxes
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="cbNum"></param>
        /// <param name="cbDate"></param>
        /// <param name="cbCharge"></param>
        public void filterData(DataGrid dg, ComboBox cbNum, ComboBox cbDate, ComboBox cbCharge)
        {
            try
            {
                DataSet myDS;
                if (cbCharge.SelectedIndex == -1 && cbDate.SelectedIndex == -1 && cbNum.SelectedIndex != -1)
                {
                    myDS = mySQL.SelectInvoiceData(cbNum.SelectedValue.ToString());
                    dg.DataContext = myDS.Tables[0].DefaultView;
                    myDS = mySQL.dateFilteredByNum(cbNum.SelectedValue.ToString());
                    int iCount = cbDate.Items.Count;
                    while (iCount > 0)
                    {
                        cbDate.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbDate.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                    myDS = mySQL.chargeFilteredByNum(cbNum.SelectedValue.ToString());
                    iCount = cbCharge.Items.Count;
                    while (iCount > 0)
                    {
                        cbCharge.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbCharge.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }

                }
                else if (cbCharge.SelectedIndex == -1 && cbNum.SelectedIndex == -1 && cbDate.SelectedIndex != -1)
                {
                    myDS = mySQL.selectInvoiceWithDate(cbDate.SelectedValue.ToString());
                    dg.DataContext = myDS.Tables[0].DefaultView;
                    myDS = mySQL.numFilteredByDate(cbDate.SelectedValue.ToString());
                    int iCount = cbNum.Items.Count;
                    while (iCount > 0)
                    {
                        cbNum.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbNum.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                    myDS = mySQL.chargeFilteredByDate(cbDate.SelectedValue.ToString());
                    iCount = cbCharge.Items.Count;
                    while (iCount > 0)
                    {
                        cbCharge.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbCharge.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                }
                else if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex == -1 && cbCharge.SelectedIndex != -1)
                {
                    myDS = mySQL.selectInvoiceWithTotalCharges(cbCharge.SelectedValue.ToString());
                    dg.DataContext = myDS.Tables[0].DefaultView;
                    myDS = mySQL.numFilteredByCharge(cbCharge.SelectedValue.ToString());
                    int iCount = cbNum.Items.Count;
                    while (iCount > 0)
                    {
                        cbNum.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbNum.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                    myDS = mySQL.dateFilteredByCharge(cbCharge.SelectedValue.ToString());
                    iCount = cbDate.Items.Count;
                    while (iCount > 0)
                    {
                        cbDate.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbDate.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                }
                else if (cbCharge.SelectedIndex != -1 && cbDate.SelectedIndex == -1 && cbNum.SelectedIndex != -1)
                {
                    myDS = mySQL.filterByNumCharge(cbNum.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                    dg.DataContext = myDS.Tables[0].DefaultView;
                    myDS = mySQL.dateFilteredByChargeNum(cbCharge.SelectedValue.ToString(), cbNum.SelectedValue.ToString());
                    int iCount = cbDate.Items.Count;
                    while (iCount > 0)
                    {
                        cbDate.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbDate.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                }
                else if (cbNum.SelectedIndex == -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex != -1)
                {
                    myDS = mySQL.filterByDateCharge(cbDate.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                    dg.DataContext = myDS.Tables[0].DefaultView;
                    myDS = mySQL.numFilteredByChargeDate(cbCharge.SelectedValue.ToString(), cbDate.SelectedValue.ToString());
                    int iCount = cbNum.Items.Count;
                    while (iCount > 0)
                    {
                        cbNum.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbNum.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                }
                else if (cbNum.SelectedIndex != -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex == -1)
                {
                    myDS = mySQL.filterByDateNum(cbDate.SelectedValue.ToString(), cbNum.SelectedValue.ToString());
                    dg.DataContext = myDS.Tables[0].DefaultView;
                    myDS = mySQL.chargeFilteredByNumDate(cbNum.SelectedValue.ToString(), cbDate.SelectedValue.ToString());
                    int iCount = cbCharge.Items.Count;
                    while (iCount > 0)
                    {
                        cbCharge.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbCharge.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                }
                else if (cbNum.SelectedIndex != -1 && cbDate.SelectedIndex != -1 && cbCharge.SelectedIndex != -1)
                {
                    myDS = mySQL.filterByDateNumCharge(cbDate.SelectedValue.ToString(), cbNum.SelectedValue.ToString(), cbCharge.SelectedValue.ToString());
                    dg.DataContext = myDS.Tables[0].DefaultView;
                    myDS = mySQL.chargeFilteredByNumDate(cbNum.SelectedValue.ToString(), cbDate.SelectedValue.ToString());
                    int iCount = cbCharge.Items.Count;
                    string selected = cbCharge.SelectedValue.ToString();
                    while (iCount > 0)
                    {
                        cbCharge.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbCharge.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                    cbCharge.SelectedValue = selected;
                    int test2 = cbCharge.SelectedIndex;
                    selected = cbNum.SelectedValue.ToString();
                    myDS = mySQL.numFilteredByChargeDate(cbCharge.SelectedValue.ToString(), cbDate.SelectedValue.ToString());
                    iCount = cbNum.Items.Count;
                    while (iCount > 0)
                    {
                        cbNum.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbNum.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                    cbNum.SelectedValue = selected;
                    selected = cbDate.SelectedValue.ToString();
                    myDS = mySQL.dateFilteredByChargeNum(cbCharge.SelectedValue.ToString(), cbNum.SelectedValue.ToString());
                    iCount = cbDate.Items.Count;
                    while (iCount > 0)
                    {
                        cbDate.Items.RemoveAt(0);
                        iCount--;
                    }
                    for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                    {
                        cbDate.Items.Add(myDS.Tables[0].Rows[i][0].ToString());
                    }
                    cbDate.SelectedValue = selected;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //code below no longer needed - replaced by filter data

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

            /// <summary>
            /// find the invoice number from the selected index and the search filters
            /// </summary>
            /// <param name="dg"></param>
            /// <param name="cbNum"></param>
            /// <param name="cbCharge"></param>
            /// <param name="cbDate"></param>
            /// <returns></returns>
        public string findInvoiceNumber(DataGrid dg, ComboBox cbNum, ComboBox cbCharge, ComboBox cbDate)
        {
            try
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
                    //string other = myDS.Tables[0].Rows[test][1].ToString(); //date
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
