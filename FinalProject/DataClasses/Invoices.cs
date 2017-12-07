using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Collections;
using System.Data;
using System.Reflection;

namespace FinalProject.Main
{
    /// <summary>
    /// holds data about singular invoice
    /// </summary>
    class InvoicesData
    {
        /// <summary>
        /// attributes for invoice data
        /// </summary>
        int invoice_Num;
        DateTime invoice_Date;
        double total_charges;

        /// <summary>
        /// property for current invoice_Num
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
        /// property for current invoice_Date
        /// </summary>
        public DateTime InvoiceDate
        {
            get
            {
                return invoice_Date;
            }
            set
            {
                invoice_Date = value;
            }
        }

        /// <summary>
        /// property for total_Charges of current invoice
        /// </summary>
        public double TotalCharges
        {
            get
            {
                return total_charges;
            }
            set
            {
                total_charges = value;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <param name="invoiceDate">invoice date</param>
        /// <param name="totalCharges">total cost of invoice</param>
        public InvoicesData(int invoiceNum, DateTime invoiceDate, double totalCharges)
        {
            invoice_Num = invoiceNum;
            invoice_Date = invoiceDate;
            total_charges = totalCharges;
        }
    }

}
