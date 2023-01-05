using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public abstract class Billabstract
    {
        
        public List<Menu> BillOrderInfo { get; set; }
        public Table BillTableInfo { get; set; }
        public DateTime BillData { get; set; }

        public string PrintBill()
        {
            Console.WriteLine("\nPrinting:\n");
            var counter = 1;
            var total = BillOrderInfo.Select(item => item.Price).Sum();
            Console.WriteLine($"Order date: \t {BillData}");
            Console.Write($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.\n");
            Console.WriteLine($"Order item:");
            foreach (var item in BillOrderInfo)
            {
                Console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            Console.WriteLine($"The order total amount = {total}Eur.");
            return "d";
        }
    }
}
