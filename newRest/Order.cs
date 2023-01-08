using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public class Order
    {
        private readonly IConsole _console;
        public DateTime OrderDate { get; set; }
        public Table Table { get; set; }
        public List<Menu> MenuItemWithPrice { get; set; } = new List<Menu>();
       

        

          

    }
}
