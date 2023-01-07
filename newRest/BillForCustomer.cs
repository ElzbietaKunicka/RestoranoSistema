﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public class BillForCustomer : BillAbstract, ISendEmail
    {
        
        public string SenderName = "RestaurantCity";
        public BillForCustomer(IConsole console)
        {
            _console = console;
        }

        public string SendEmail()
        {
            _console.WriteLine("\nPlease enter email");
            var recipientEmail = _console.ReadString();
            var counter = 1;
            var total = BillOrderInfo.Select(item => item.Price).Sum();
            _console.WriteLine($"\nOrder date: \t {BillData}");
            _console.WriteLine($"Table ID: {BillTableInfo.TableId},\nNumberOfSeats: {BillTableInfo.NumberOfSeats}.\n");
            _console.WriteLine($"Order item:");
            foreach (var item in BillOrderInfo)
            {
                _console.WriteLine($"\t{counter++}.{item.Name}\t{item.Price}eur.");
            }
            _console.WriteLine($"The order total amount = {total}Eur.");
            return $"...Email has been successfully sent to: {recipientEmail}";
        }
    }
    
}