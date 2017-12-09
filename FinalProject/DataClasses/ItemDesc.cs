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
    /// <summary>
    /// holds data about singular inventory item
    /// </summary>
    class ItemDescData
    {
        /// <summary>
        /// attributes for itemDesc data
        /// </summary>
        string item_Code;
        string item_Desc;
        Decimal cost;

        /// <summary>
        /// prperty for current item_Code
        /// </summary>
        public string ItemCode
        {
            get
            {
                return item_Code;
            }
            set
            {
                item_Code = value;
            }
        }

        /// <summary>
        /// property for current item_Desc
        /// </summary>
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


        /// <summary>
        /// property for current item's cost
        /// </summary>
        public Decimal Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="itemCode">code for specific item</param>
        /// <param name="itemDesc">description for specific item</param>
        /// <param name="itemCost">cost of item</param>
        public ItemDescData (string itemCode, string itemDesc, Decimal itemCost)
        {
            item_Code = itemCode;
            item_Desc = itemDesc;
            cost = itemCost;
        }


        //public string SaveItemToDB()
        //{
        //    int iRetVal = 0;

        //    clsDataAccess DataAccess = new clsDataAccess();

        //    string SQL = "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES('" + item_Code + "', '" + item_Desc + "', " + cost + ")";

        //    iRetVal = DataAccess.ExecuteNonQuery(SQL);

        //    return ItemCode;
        //}

        public override string ToString()
        {
            return ItemDesc;
        }
    }
}
