using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using FinalProject.DataClasses;

namespace FinalProject.Main
{
    class clsMainFunctions
    {
        //Invoices invoices = new Invoices();

        /// <summary>
        /// Method that calls delete query from clsMainSQL
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        public void DeleteInvoice(int invoiceNum)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                sql.DelInvoice(invoiceNum);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        public void LoadAllInvoices(InvoicesData all)
        {
     //       foreach (InvoicesData data in invoices.LoadAll())
      //      {
       //         cbChoosePassenger.Items.Add(data);
//            }

        }

        public void AddInvoice(double totalCharge, DateTime invoiceDate)
        {
            try
            {
                clsMainSQL sql = new clsMainSQL();
                sql.AddInvoice(totalCharge, invoiceDate);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
    }   
}
