using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public class BillForRestaurant : BillAbstract, ISendEmail
    {
        private readonly IConsole _console;
        public string SenderName = "RestaurantCity";
        public string RecipientEmail { get; set; }
        public string _invoice { get; set; }
      
        public BillForRestaurant(IConsole console)
        {
            _console = console;
        }
        public string PrintBill()
        {
            _console.WriteLine("\n\tPrinting Bill For Restaurant:\n");
            var items = BillOrderInfo;
            var counter = 1;
            var tableInfo = BillTableInfo;
            var total = items.Select(item => item.Price).Sum();
            _console.WriteLine($"Invoice:\t{GenerateInvoiceNumber()}.");
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
            _console.WriteLine("\tPayment is successful");
            return "Payment is successful\n";
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
            _console.WriteLine("...Data has been saved successfully");
            string json = JsonSerializer.Serialize(this);
            //File.WriteAllText("dataa.txt", json);
            File.AppendAllText("dataa.txt", json);
            
            return "Saved in file";
        }

        public string SendEmail()
        {
            _console.WriteLine("\nPlease enter email to send Restaurant Bill");
            var recipientEmail = _console.ReadString();
            var counter = 1;
            var total = BillOrderInfo.Select(item => item.Price).Sum();
            _console.WriteLine($"\nInvoice:\t{GenerateInvoiceNumber()}.");
            _console.WriteLine($"Order date: \t {BillData}");
            _console.WriteLine($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.\n");
            _console.WriteLine($"Order item:");
            foreach (var item in BillOrderInfo)
            {
                _console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            _console.WriteLine($"The order total amount = {total}Eur.");
            double vat = Convert.ToDouble(total) * 21 / 100;
            _console.WriteLine($"Vat: {vat}eur");
            _console.WriteLine($"\nEmail has been successfully sent to: {recipientEmail}");

            return $"Subject: Restaurant Bill\n \tEmail has been successfully sent to: {recipientEmail}";
        }
    }
}
