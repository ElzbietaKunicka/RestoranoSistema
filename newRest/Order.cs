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
        public Table NumberOfSeats { get; set; }
        public List<Menu> MenuItemWithPrice { get; set; } = new List<Menu>();
        public int TotalOrderAmount { get; set; }

    }
}
