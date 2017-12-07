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
            populateDataGrid(); ;
        }

        private void populateDataGrid()
        {
            grdItems.ItemsSource = AutoFillDataGrid();
            grdItems.AutoGenerateColumns = true;
            grdItems.ColumnWidth = 100;
            grdItems.Background = new SolidColorBrush(Colors.LightGray);
            grdItems.AlternatingRowBackground = new SolidColorBrush(Colors.LightBlue);
            grdItems.RowBackground = new SolidColorBrush(Colors.LightGoldenrodYellow);
            grdItems.SelectionMode = DataGridSelectionMode.Single;
        }

        private IList<clsItems> AutoFillDataGrid()
        {
            /*
             *call business logic for retrieving data from the invoices database.
             */
            clsEditSQL es = new clsEditSQL();
            DataSet ds;
            IList<clsItems> items = new List<clsItems>();
            items.Clear();
            items = es.pullItemsTable();
            //grdItems.ItemsSource = ds.DefaultViewManager;

            return items;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Please confirm the save operation!", "Edit Item Confirmation", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    clsEditSQL es = new clsEditSQL();
                    es.updateItems(txbItemCode.Text, txbItemDesc.Text, txbCost.Text);
                    break;
                case MessageBoxResult.Cancel:
                    MainWindow mw = new MainWindow();
                    mw.ShowDialog();
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
            clsItems row_selected = gd.SelectedItem as clsItems;
            if(row_selected != null)
            {
                txbItemCode.Text = row_selected.Item_Code.ToString();
                txbItemDesc.Text = row_selected.Item_Desc.ToString();
                txbCost.Text = row_selected.Cost.ToString();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string itemcode = txbItemCode.Text;
            clsEditSQL es = new clsEditSQL();
            //If not found in existing invoices
            if (es.DeleteItem(itemcode))
            {
                txbCost.Text = "";
                txbItemCode.Text = "";
                txbItemDesc.Text = "";
                AutoFillDataGrid();
            }
        }
    }
}
