using System;
using System.Collections.Generic;
using System.Linq;
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

namespace FinalProject
{
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
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            wndSearchForm.ShowDialog();
            this.Show();

        }

        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            //this function will access a specified record (through txbInvNum) from the database.
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            //this will delete a row from LineItems as specified by the invoice number, and LineItemNum accessed through txbInvNum and dgInvoiceItems by calling deleteInvoiceItem function in clsMainSQL
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            //this will add a row to LineItems as specified by the invoice number and LineItemNum accessed through txbInvNum and dgInvoiceItems by calling addInvoiceItem function in clsMainSQL

        }

        private void btnAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            //this will add a row to the invoices table using the InvoiceDate from txbInvDate, the TotalCharge from txbInvTotal by calling the addInvoice function in clsMainSQL
        }

        //cmbItems will be populated with Inventory data through allInventoryItems function in clsMainSQL

        //txbItmeCost is a read only and will be populateed with Cost from ItemDesc in database using itemCost function in clsMainSQL

        //txbInvTotal will be populated by adding all costs from dgInvoiceItems


    }
}
