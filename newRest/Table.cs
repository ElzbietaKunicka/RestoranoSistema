using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newRest
{
    public class Table
    {
        public int TableId { get; set; }
        public int NumberOfSeats { get; set; }
        public string TableState { get; set; }

        public List<Order> OrderHistory { get; set; }

        //public Table(int tableId, int numberOfSeats)
        //{
        //    TableId = tableId;
        //    NumberOfSeats = numberOfSeats;
        //}
    }
}
