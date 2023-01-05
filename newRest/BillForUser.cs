using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public class BillForUser : Billabstract, ISendEmail
    {
        public string SenderName = "RestaurantCity";

        public string SendEmail(Billabstract bill)
        {
            Console.WriteLine("\nPlease enter email");
            var recipientEmail = (Console.ReadLine().ToString());
            var counter = 1;
            var total = BillOrderInfo.Select(item => item.Price).Sum();
            Console.WriteLine($"\nOrder date: \t {BillData}");
            Console.Write($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.\n");
            Console.WriteLine($"Order item:");
            foreach (var item in BillOrderInfo)
            {
                Console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            Console.WriteLine($"The order total amount = {total}Eur.");
            return $" email has been successfully sent to: {recipientEmail}";
        }
    }
    
}
