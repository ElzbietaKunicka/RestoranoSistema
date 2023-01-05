using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public class BillForRestaurant : Billabstract, ISendEmail
    {
        public string SenderName = "RestaurantCity";
        public string _invoice { get; set; }
      
        
        

        public string PrintBill()
        {
            Console.WriteLine("\nPrinting:\n");
            var items = BillOrderInfo;
            var counter = 1;
            var tableInfo = BillTableInfo;
            var total = items.Select(item => item.Price).Sum();
            Console.WriteLine($"Invoice:\t{GenerateInvoiceNumber()}.");
            Console.WriteLine($"Order date: \t {BillData}");
            Console.Write($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.\n");
            Console.WriteLine($"Order item:");
            foreach (var item in items)
            {
                Console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            Console.WriteLine($"The order total amount = {total}Eur.");
            double vat = Convert.ToDouble(total) * 21 / 100;
            Console.WriteLine($"Vat: {vat}eur");
            Console.WriteLine("Paid");
            //Save();
            return "save in file";
        }
        

        public string GenerateInvoiceNumber()
        {
            var rnd = new Random().Next(1, 50).ToString();
            char pad = '0';
            var invoice = ("E" + rnd.PadLeft(6, pad));
            this._invoice = invoice;
            return invoice;
        }
        public string Save() 
        {
            string json = JsonSerializer.Serialize(this);
            //File.WriteAllText("dataa.txt", json);
            File.AppendAllText("dataa.txt", json);
            return "save";
        }

        

        public string SendEmail(Billabstract bill)
        {
            Console.WriteLine("\nPlease enter email");
            var recipientEmail = (Console.ReadLine().ToString());
            var counter = 1;
            var total = BillOrderInfo.Select(item => item.Price).Sum();
            Console.WriteLine($"\nInvoice:\t{GenerateInvoiceNumber()}.");
            Console.WriteLine($"Order date: \t {BillData}");
            Console.Write($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.\n");
            Console.WriteLine($"Order item:");
            foreach (var item in BillOrderInfo)
            {
                Console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            Console.WriteLine($"The order total amount = {total}Eur.");
            double vat = Convert.ToDouble(total) * 21 / 100;
            Console.WriteLine($"Vat: {vat}eur");



            return $" email has been successfully sent to: {recipientEmail}";
        }
    }
}
