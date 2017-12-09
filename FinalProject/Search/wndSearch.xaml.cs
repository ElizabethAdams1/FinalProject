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
        string selectedNum;
        /// <summary>
        /// property for selectedid
        /// </summary>
        public string SelectedNum
        {
            get
            {
                return selectedNum;
            }
            set
            {
                selectedNum = value;
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
            if (cBNum.SelectedIndex > -1 && cBNum.SelectedValue != null)
            {
                string value = cBNum.SelectedValue.ToString();
                //searchFunct.filterByNum(value, dGInvoices, cBDate, cBCharge);
                searchFunct.filterData(dGInvoices, cBNum, cBDate, cBCharge);
            }
        }
        /// <summary>
        /// When the date selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //filter based on new selection
            if (cBDate.SelectedIndex > -1 && cBDate.SelectedValue != null)
            {
                string value = cBDate.SelectedValue.ToString();
                //searchFunct.filterByDate(value, dGInvoices, cBNum, cBCharge);
                searchFunct.filterData(dGInvoices, cBNum, cBDate, cBCharge);
            }
        }
        /// <summary>
        /// When the total charge selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //filter based on new selection
            if (cBCharge.SelectedIndex > -1 && cBCharge.SelectedValue != null)
            {
                string value = cBCharge.SelectedValue.ToString();
                //searchFunct.filterByTotalCharges(value, dGInvoices, cBNum, cBDate);
                searchFunct.filterData(dGInvoices, cBNum, cBDate, cBCharge);
            }
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
            if (dGInvoices.SelectedCells.Count != 0)
            {

                string num = searchFunct.findInvoiceNumber(dGInvoices, cBNum, cBCharge, cBDate);
                selectedNum = num;
                this.Hide();
            }
            
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
            cBCharge.SelectedIndex = -1;
            cBNum.SelectedIndex = -1;
            cBDate.SelectedIndex = -1;
            searchFunct.fillInvoiceDates(cBDate);
            searchFunct.fillInvoiceNumbers(cBNum);
            searchFunct.fillInvoiceTotalCharges(cBCharge);
            searchFunct.fillInvoices(dGInvoices);
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
            //cBCharge.

            //to reset selected value
            cBCharge.SelectedIndex = -1;
            cBNum.SelectedIndex = -1;
            cBDate.SelectedIndex = -1;
            searchFunct.fillInvoiceDates(cBDate);
            searchFunct.fillInvoiceNumbers(cBNum);
            searchFunct.fillInvoiceTotalCharges(cBCharge);
            searchFunct.fillInvoices(dGInvoices);
        }
    }
}
