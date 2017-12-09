using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DataClasses
{
    class InvoiceDetailsData
    {
        int invoice_Num;
        string item_Desc;
        int line_Item_Number;
        decimal item_Cost;

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

        public string ItemDesc
        {
            get
            {
                return item_Desc;
            }
            set
            {
                item_Desc = value; 
            }
        }

        public int LineItemNumber
        {
            get
            {
                return line_Item_Number;
            }
            set
            {
                line_Item_Number = value;
            }
        }

        public decimal ItemCost
        {
            get
            {
                return item_Cost;
            }
            set
            {
                item_Cost = value;
            }
        }

        public InvoiceDetailsData(int invoiceNum, string itemDesc, int lineItemNumber, decimal itemCost)
        {
            invoice_Num = invoiceNum;
            item_Desc = itemDesc;
            item_Cost = itemCost;
            line_Item_Number = lineItemNumber;
        }
    }


}
