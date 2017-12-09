using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using FinalProject.Main;
using FinalProject.DataClasses;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Search.wndSearch wndSearchForm;
        
        /// <summary>
        /// Main Window Constructor
        /// </summary>
        public MainWindow()
        {
            try
            {

                InitializeComponent();
                wndSearchForm = new Search.wndSearch();

                //cmbItems will be populated with Inventory data through LoadAllInventoryItems function in clsMainSQL

                clsMainFunctions getInv = new clsMainFunctions();
                foreach (ItemDescData data in getInv.LoadAllInventoryItems())
                {
                    cmbItems.Items.Add(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// loads search window when "search for invoice" is clicked from corner menu of main window
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                wndSearchForm.ShowDialog();
                if (wndSearchForm.SelectedNum != null)
                {
                    txbInvNum.Text = wndSearchForm.SelectedNum;
                }
                this.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        /// <summary>
        /// loads search window when "edit item list" is clicked from corner menu of main window
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                EditPage ep = new EditPage();
                ep.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        /// <summary>
        /// Link from Edit in menu to Edit window
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">event args</param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //this function will access a specified record (through txbInvNum) from the database.
                int invoiceNum;
                clsMainFunctions funcs = new clsMainFunctions();

                //loads all details for specific invoice into datagrid
                if (int.TryParse(txbInvNum.Text, out invoiceNum))
                {
                    InvoiceDetailsData[] ds = funcs.LoadInvoiceDetails(invoiceNum);
                    dgInvoiceItems.ItemsSource = ds;
                    decimal sumTotalCost = GetInvoiceTotalFromDataGrid();

                    txbInvTotal.Text = sumTotalCost.ToString("$0.00");
                }
                else
                {
                    MessageBox.Show("invalid invoice number");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }
        /// <summary>
        /// gets the invoice total and loads it into textbox
        /// </summary>
        /// <returns>total cost of invoice</returns>
        private Decimal GetInvoiceTotalFromDataGrid()
        {
            try
            {
                Decimal sumTotalCost = 0.0m;
                foreach (object data in dgInvoiceItems.Items)
                {
                    sumTotalCost += ((InvoiceDetailsData)data).ItemCost;
                }

                return sumTotalCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        /// <summary>
        /// this will delete a row from LineItems as specified by the invoice number, and LineItemNum accessed through txbInvNum and dgInvoiceItems by calling deleteInvoiceItem function in clsMainSQL
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //add column to datagrid for item costs        
                if (dgInvoiceItems.SelectedItem != null)
                {
                    clsMainFunctions funcs = new clsMainFunctions();
                    InvoiceDetailsData data = (InvoiceDetailsData)dgInvoiceItems.SelectedItem;
                    funcs.DeleteInvoiceItem(data.LineItemNumber, data.InvoiceNum);

                    // Reload DataGrid from Database.
                    InvoiceDetailsData[] ds = funcs.LoadInvoiceDetails(data.InvoiceNum);
                    dgInvoiceItems.ItemsSource = ds;

                    // Update the invoice total textbox.
                    Decimal sumTotalCost = GetInvoiceTotalFromDataGrid();
                    txbInvTotal.Text = sumTotalCost.ToString("$0.00");

                    // Update the database invoice total.
                    funcs.UpdateInvoiceTotal(data.InvoiceNum, sumTotalCost);

                }
                else
                {
                    MessageBox.Show("Please select an item to remove");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        /// <summary>
        /// this will add a row to LineItems as specified by the invoice number and LineItemNum accessed through txbInvNum and dgInvoiceItems by calling addInvoiceItem function in clsMainSQL
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsMainFunctions businessLogic = new clsMainFunctions();
                ItemDescData data = (ItemDescData)cmbItems.SelectedItem;
                int invoiceNum = 0;

                //checks for validity of invoice number
                if (!int.TryParse(txbInvNum.Text, out invoiceNum))
                {
                    MessageBox.Show("Invalid invoice Number");
                    return;
                }

                //adds item to invoice
                businessLogic.AddInvoiceItem(invoiceNum, data.ItemCode);

                // Reload DataGrid from Database.
                InvoiceDetailsData[] ds = businessLogic.LoadInvoiceDetails(invoiceNum);
                dgInvoiceItems.ItemsSource = ds;

                // Update the invoice total textbox.
                Decimal sumTotalCost = GetInvoiceTotalFromDataGrid();
                txbInvTotal.Text = sumTotalCost.ToString("$0.00");

                // Update the database invoice total.
                businessLogic.UpdateInvoiceTotal(invoiceNum, sumTotalCost);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        /// <summary>
        /// method that calls AddInvoice from clsMainFunctions to add invoice to db. this will add a row to the invoices table using the InvoiceDate from txbInvDate, the TotalCharge from txbInvTotal by calling the addInvoice function in clsMainSQL
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void btnAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                clsMainFunctions businessLogic = new clsMainFunctions();

                double totalCost;
                DateTime invoiceDate;

                //tests for validity of invoice and dates
                if (double.TryParse(txbInvTotal.Text, out totalCost))
                {
                    if (DateTime.TryParse(txbInvDate.Text, out invoiceDate))
                    {
                        InvoicesData invoice = new InvoicesData(-1, invoiceDate, totalCost);

                        //adds invoice to db
                        businessLogic.AddInvoice(totalCost, invoiceDate);
                        MessageBox.Show("Invoice has been saved.");
                    }
                }
                else
                {
                    MessageBox.Show("invalid invoice number");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        /// <summary>
        /// method that calls DeleteInvoice from clsMainFunctions to delete invoice from db.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsMainFunctions businessLogic = new clsMainFunctions();
                int invoiceNum;

                //tests for validity of invoice number 
                if (int.TryParse(txbInvNum.Text, out invoiceNum))
                {
                    //deletes invoice from db
                    businessLogic.DeleteInvoice(invoiceNum);
                    MessageBox.Show("Invoice " + invoiceNum + " has been deleted.");
                }
                else
                {
                    MessageBox.Show("invalid invoice number");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        //Converts item cost of selected item in combobox to string
        private void cmbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txbItemCost.Text = ((ItemDescData)cmbItems.SelectedItem).Cost.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

    }
}
