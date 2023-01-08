using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public class BillForRestaurant : Bill, ISendEmail
    {
        private readonly IConsole _console;
        
        public string _invoice { get; set; }
      
        public BillForRestaurant(IConsole console)
        {
            _console = console;
        }
        public string PrintBill()
        {
            _console.WriteLine("\nPrinting Bill For Restaurant:");
            _console.WriteLine("_____________________________________");
            var items = BillOrderInfo;
            var counter = 1;
            var tableInfo = BillTableInfo;
            var total = items.Select(item => item.Price).Sum();
            _console.WriteLine($"\nInvoice:\t{GenerateInvoiceNumber()} ");
            _console.WriteLine($"Order date: \t {BillData}");
            _console.WriteLine($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.");
            _console.WriteLine($"Order item:");
            foreach (var item in items)
            {
                _console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            _console.WriteLine($"The order total amount = {total}Eur.");
            double vat = Convert.ToDouble(total) * 21 / 100;
            _console.WriteLine($"Vat: {vat}eur");
            _console.WriteLine("  ***Payment is successful***");
            
            _console.WriteLine("_____________________________________");
            return "Payment is successful";
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
            _console.WriteLine("\nData has been saved successfully");
            string json = JsonSerializer.Serialize(this);
            File.AppendAllText("dataa.txt", json);
            return "Saved in file";
        }

        public string SendEmail()
        {
            _console.WriteLine("\nPlease enter email (Restaurant Bill)");
            var recipientEmail = _console.ReadString();
            var counter = 1;
            var total = BillOrderInfo.Select(item => item.Price).Sum();
            _console.WriteLine($"\nInvoice:\t{GenerateInvoiceNumber()}.");
            _console.WriteLine($"Order date: \t {BillData}");
            _console.WriteLine($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.");
            _console.WriteLine($"Order item:");
            foreach (var item in BillOrderInfo)
            {
                _console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            _console.WriteLine($"The order total amount = {total}Eur.");
            double vat = Convert.ToDouble(total) * 21 / 100;
            _console.WriteLine($"Vat: {vat}eur");
            _console.WriteLine($"\n***Email has been successfully sent to: {recipientEmail}***");
            return $"Subject: Restaurant Bill\n Email has been successfully sent to: {recipientEmail}";
        }
    }
}
