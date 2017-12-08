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
    //Flights flights = new Flights(); // instantiates Flights
    //Passengers passenger = new Passengers(); //instantiates Passengers
    //FlightPassengers flightPass = new FlightPassengers();

   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Search.wndSearch wndSearchForm;
        
        public MainWindow()
        {
            InitializeComponent();
            wndSearchForm = new Search.wndSearch();

            //cmbItems will be populated with Inventory data through LoadAllInventoryItems function in clsMainSQL

            clsMainFunctions getInv = new clsMainFunctions();
  //          DataSet ds = getInv.LoadAllInventoryItemsAsDataSet();
            foreach (ItemDescData data in getInv.LoadAllInventoryItems())
            {
                cmbItems.Items.Add(data);
            }

            //private void edit_Click(object sender, RoutedEventArgs e)


            //clsMainFunctions funcs = new clsMainFunctions();

            //    if (int.TryParse(txbInvNum.Text, out invoiceNum))
            //    {
            //        DataSet ds = funcs.LoadAllInvoiceItemsAsDataSet(invoiceNum);
            //dgInvoiceItems.ItemsSource = ds.Tables[0].DefaultView;
            //    }
            //    else
            //    {
            //        MessageBox.Show("invalid invoice number");
            //    }

        }

        /// <summary>
        /// loads search window when "search for invoice" is clicked from corner menu of main window
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void search_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            wndSearchForm.ShowDialog();
            this.Show();

        }

        /// <summary>
        /// loads search window when "edit item list" is clicked from corner menu of main window
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void edit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            EditPage ep = new EditPage();
            ep.Show();
        }

        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            //this function will access a specified record (through txbInvNum) from the database.
            int invoiceNum;
            clsMainFunctions funcs = new clsMainFunctions();
           // int colSum;

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

        private Decimal GetInvoiceTotalFromDataGrid()
        {
            Decimal sumTotalCost = 0.0m;
            foreach (object data in dgInvoiceItems.Items)
            {
                sumTotalCost += ((InvoiceDetailsData)data).ItemCost;
            }

            return sumTotalCost;
        }


        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            //this will delete a row from LineItems as specified by the invoice number, and LineItemNum accessed through txbInvNum and dgInvoiceItems by calling deleteInvoiceItem function in clsMainSQL

    //********************************************* check invoice total to match item editing in ti
            //add column to datagrid for item costs        
            if(dgInvoiceItems.SelectedItem != null)
            {
                clsMainFunctions funcs = new clsMainFunctions();
                InvoiceDetailsData data = (InvoiceDetailsData) dgInvoiceItems.SelectedItem;
                funcs.DeleteInvoiceItem(data.LineItemNumber, data.InvoiceNum);



      ////          funcs.UpdateInvoiceTotal(invD)
          //      InvoicesData invData = (InvoicesData)txbInvTotal.


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

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            //this will add a row to LineItems as specified by the invoice number and LineItemNum accessed through txbInvNum and dgInvoiceItems by calling addInvoiceItem function in clsMainSQL

        }

        /// <summary>
        /// method that calls AddInvoice from clsMainFunctions to add invoice to db. this will add a row to the invoices table using the InvoiceDate from txbInvDate, the TotalCharge from txbInvTotal by calling the addInvoice function in clsMainSQL
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void btnAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            clsMainFunctions businessLogic = new clsMainFunctions();

            double totalCost;
            DateTime invoiceDate;

            if (double.TryParse(txbInvTotal.Text, out totalCost))
            {
                if(DateTime.TryParse(txbInvDate.Text, out invoiceDate))
                {
                    InvoicesData invoice = new InvoicesData(-1, invoiceDate, totalCost);
                    businessLogic.AddInvoice(totalCost, invoiceDate);
                    MessageBox.Show("Invoice has been saved.");
                }
            }
            else
            {
                MessageBox.Show("invalid invoice number");
            }


        }

        /// <summary>
        /// method that calls DeleteInvoice from clsMainFunctions to delete invoice from db.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            clsMainFunctions businessLogic = new clsMainFunctions();
            int invoiceNum;
            if (int.TryParse(txbInvNum.Text, out invoiceNum))
            {
                businessLogic.DeleteInvoice(invoiceNum);
                MessageBox.Show("Invoice " + invoiceNum + " has been deleted.");
            }else
            {
                MessageBox.Show("invalid invoice number");
            }

        }

        private void cmbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //decimal cost = ((ItemDescData)cmbItems.SelectedItem).Cost;
            //txbItemCost = cost.ToString();

            txbItemCost.Text = ((ItemDescData)cmbItems.SelectedItem).Cost.ToString();
        }

        private void dgInvoiceItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///not sure if i need this
        }

        //public int CalculateTotalCost(int invoiceNum)
        //{
        //    invoiceNum = 
        //}







        //txbItmeCost is a read only and will be populateed with Cost from ItemDesc in database using itemCost function in clsMainSQL

        //txbInvTotal will be populated by adding all costs from dgInvoiceItems


    }
}
