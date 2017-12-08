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

namespace FinalProject.Search
{
    ///
    /// 
    ///GENERAL NOTES FROM REBECCA:
    ///Anything that I don't know if it will be called by something else,
    ///I have not yet implemented exception handling
    ///because I would like to coordinate what we want to do to the top-most event
    ///handling the exception
    /// 
    /// 
    
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// selectedID for main
        /// </summary>
        string selectedId;
        /// <summary>
        /// property for selectedid
        /// </summary>
        public string SelectedId
        {
            get
            {
                return selectedId;
            }
            set
            {
                selectedId = value;
            }
        }
        clsSearchFunctions searchFunct = new clsSearchFunctions();
        /// <summary>
        /// constructor
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            searchFunct.fillInvoiceDates(cBDate);
            searchFunct.fillInvoiceTotalCharges(cBCharge);
            searchFunct.fillInvoiceNumbers(cBNum);
            searchFunct.fillInvoices(dGInvoices);
        }
        /// <summary>
        /// When the Number selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Filter based on new selection
        }
        /// <summary>
        /// When the date selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //filter based on new selection
        }
        /// <summary>
        /// When the total charge selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //filter based on new selection
        }
        /// <summary>
        /// When the select button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            //the selected invoice id needs to be set to a variable in the search window,
            //the main window will access this data via the property SelectedId,
            //and the search window will close and the main window will open
            this.Hide();
        }
        /// <summary>
        /// when the cancel button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //closes the search window and opens the main window
            //should not perform any of the same actions select would, setting selectedId, etc.
            this.Hide();
        }
        /// <summary>
        /// When the Clear Selection button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            //clears all selections on dropdowns, and set page to default
        }
    }
}
