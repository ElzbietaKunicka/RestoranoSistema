using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newRest
{
    public class Bill
    {
        public Order Order { get; set; }
        public List<string> Print { get; set; }
    }
}
