using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public abstract class BillAbstract
    {
        public  IConsole _console;
        public List<Menu> BillOrderInfo { get; set; }
        public Table BillTableInfo { get; set; }
        public DateTime BillData { get; set; }
        

        public string PrintBill()
        {
            _console.WriteLine("\n\tPrinting:\n");
            var counter = 1;
            var total = BillOrderInfo.Select(item => item.Price).Sum();
            _console.WriteLine($"Order date: \t {BillData}");
            _console.WriteLine($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.");
            _console.WriteLine($"Order item:");
            foreach (var item in BillOrderInfo)
            {
                _console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            _console.WriteLine($"The order total amount = {total}Eur.");
            _console.WriteLine("\tThank you, have a nice day.");
            return "Thank you, have a nice day.";
        }

       
    }
}
