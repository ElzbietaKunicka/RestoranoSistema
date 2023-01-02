using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newRest
{
    public abstract class Billabstract
    {
        
        public List<Menu> BillOrderInfo { get; set; }
        public Table BillTableInfo { get; set; }
        public DateTime BillData { get; set; }

        public abstract void PrintBill();
    }
}
