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
using System.Windows.Shapes;
using System.Data;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Window
    {
        public EditPage()
        {
            InitializeComponent();
            //AutoFillDataGrid();
        }

        private void AutoFillDataGrid()
        {
            /*
             *call business logic for retrieving data from the invoices database.
             */
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = new DataSet();
            ds = bl.pullTable("itemdesc");
            grdItems.DataContext = ds;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Please confirm the save operation!", "Edit Item Confirmation", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    //BusinessLogic bl = new BusinessLogic();
                    //bl.updateSQL(txbItemCode.Text, txbItemDesc.Text, txbCost.Text);
                    MessageBox.Show("Hello to you too!", "My App");
                    break;
                case MessageBoxResult.Cancel:
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                    break;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void grdItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected != null)
            {
                txbItemCode.Text = row_selected["Item Code"].ToString();
                txbItemDesc.Text = row_selected["Item Description"].ToString();
                txbCost.Text = row_selected["Cost"].ToString();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string itemcost = txbItemCode.Text;
            BusinessLogic bl = new BusinessLogic();
            //If not found in existing invoices
            bl.DeleteItem(itemcost);
        }
    }
}
