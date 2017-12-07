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
using System.Reflection;
using System.Data.OleDb;
using System.Data;

namespace FinalProject.DataClasses
{
    class LineItemsData
    {
        /// <summary>
        /// attributes for lines in invoice
        /// </summary>
        int invoice_Num;
        int line_Item_Num;
        string item_code;

        /// <summary>
        /// property for invoice num of current line
        /// </summary>
        public int InvoiceNum
        {
            get
            {
                return invoice_Num;
            }
            set
            {
                invoice_Num = value;
            }
        }

        /// <summary>
        /// property for line that the item is on in invoice
        /// </summary>
        public int LineItemNumber
        {
            get
            {
                return line_Item_Num;
            }
            set
            {
                line_Item_Num = value;
            }
        }

        /// <summary>
        /// property for item code of current line in invoice
        /// </summary>
        public string ItemCode
        {
            get
            {
                return item_code;
            }
            set
            {
                item_code = value;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="invoiceNum">invoice number for item</param>
        /// <param name="lineItemNum">line on invoice for itemparam>
        /// <param name="itemCode">item code</param>
        public LineItemsData(int invoiceNum, int lineItemNum, string itemCode)
        {
            invoice_Num = invoiceNum;
            line_Item_Num = lineItemNum;
            item_code = itemCode;
        }


        public int SaveInvoiceLineToDB()
        {
            int iRetVal = 0;

            clsDataAccess DataAccess = new clsDataAccess(); // new instance of clsDataAccess for opening connection


            string SQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES(" + invoice_Num + ", " + line_Item_Num + ", '" + item_code + "')";

            iRetVal = DataAccess.ExecuteNonQuery(SQL); // SQL statement to save information from Flight table


            return invoice_Num;
        }

    }
}
