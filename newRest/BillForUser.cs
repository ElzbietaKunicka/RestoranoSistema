using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newRest
{
    public class BillForUser : Billabstract
    {
        public List<Menu> BillOrderInfo { get; set; }
        public Table BillTableInfo { get; set; }
        public DateTime BillData { get; set; }

       

        public override void PrintBill()
        {
            var items = BillOrderInfo;
            var counter = 1;
            //var total = items.Select(item => item.Price).Sum();
            Console.WriteLine(" ");
            //_console.WriteLine($" Table ID: {TableInfo.TableId}, NumberOfSeats: {TableInfo.NumberOfSeats} seats.");
            Console.WriteLine(" ");
            foreach (var item in items)
            {
                Console.WriteLine($"   {counter++}.{item.Name}   {item.Price}eur.");

            }
            //Console.WriteLine($"The order total amount is {total} eur.");
            //_console.WriteLine("");
            Console.WriteLine($"Order date: {BillData}"); 
        }
    }
}
