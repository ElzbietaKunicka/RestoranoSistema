using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace newRest
{
    public class Order
    {
        
        public DateTime OrderDate { get; set; }

        public Table TableId { get; set; }
        public List<Menu> ItemWithPrice { get; set; }

        //public List<Menu> Full { get; set; }



        //public Order(string name, int price)
        //{
        //   this.DName = name;
        //   this.DPrice = price;
        //}

        //public Order(Table table, List<Menu> full)
        //{
        //    this.Table = table;
        //    Full = new List<Menu>();
        //}

       
    }
}
