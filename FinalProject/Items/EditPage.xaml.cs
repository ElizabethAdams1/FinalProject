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
using FinalProject.DataClasses;
using System.ComponentModel;

namespace FinalProject
{
    /// <summary>
    /// Quinn Anderson
    /// CS3280
    /// Group Project 
    /// User Interaction logic for EditPage.xaml
    /// This handles all of the user interaction as well as 
    /// gathering information from clsEditSQL to populate
    /// the datagrid.
    /// Also I added all the blending and styles for the application
    /// </summary>
    public partial class EditPage : Window
    {
        //Initializes the Editpage
        public EditPage()
        {
            InitializeComponent();
            populateDataGrid(); ;
        }

        /// <summary>
        /// populates the datagrid and sets certain properties
        /// this method is called repeatedly to refresh the datagrid
        /// </summary>
        private void populateDataGrid()
        {
            grdItems.ItemsSource = AutoFillDataGrid();
            grdItems.AutoGenerateColumns = false;
            grdItems.SelectionMode = DataGridSelectionMode.Single;
        }

        /// <summary>
        /// This method handles the data returned from running the SQL.
        /// </summary>
        /// <returns></returns>
        private IList<ItemDescData> AutoFillDataGrid()
        {
            /*
             *call business logic for retrieving data from the invoices database.
             */
            clsEditSQL es = new clsEditSQL();
            IList<ItemDescData> items = new List<ItemDescData>();
            items.Clear();
            items = es.pullItemsTable();
            //grdItems.ItemsSource = ds.DefaultViewManager;

            return items;

        }

        /// <summary>
        /// This method saves the information in the textboxes to the database.
        /// the logic is handled in the other class.
        /// The user is asked to confirm their choice prior to saving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool sqlRel = false;
            MessageBoxResult result = MessageBox.Show("Please confirm the save operation!", "Edit Item Confirmation", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    clsEditSQL es = new clsEditSQL();
                    sqlRel = es.updateItems(txbItemCode.Text, txbItemDesc.Text, txbCost.Text);
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            if (sqlRel == true)
            {
                populateDataGrid();
            }
        }

        /// <summary>
        /// This Method simply closes the window and goes back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        /// <summary>
        /// This method handles when an item is seleted from the 
        /// datagrid.  The values are translated over to the textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            ItemDescData row_selected = gd.SelectedItem as ItemDescData;
            if(row_selected != null)
            {
                txbItemCode.Text = row_selected.ItemCode.ToString();
                txbItemDesc.Text = row_selected.ItemDesc.ToString();
                txbCost.Text = row_selected.Cost.ToString();
            }
        }

        /// <summary>
        /// This method handles the deletion of an item from the 
        /// datagrid.  It passes the SQL request to the clsEditSQL class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                populateDataGrid();
            }
        }

        /// <summary>
        /// this method ensures that the MainWindow is
        /// open if user closes the EditPage window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            base.OnClosing(e);
        }
    }
}
