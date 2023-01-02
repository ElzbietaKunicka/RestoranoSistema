using System;
using System.Runtime.CompilerServices;

namespace newRest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var customConsole = new CustomConsole();
            var mainApp = new MainApp(customConsole);
           if (true)
            {
              var apLog = mainApp.Login();
                if(apLog == false)
                {
                    return;
                }
            }
            
            //var total = mainApp.CountTotalOrderAmount();
           
            var newOrder = new Order();
            var table = mainApp.ChooseTable();
            if(table.TableId == 0)
            {
                return;
            }
            if(table.TableId != 0)
            {
                newOrder.Table = table;
                //newOrder.TableId = table; change name
                newOrder.OrderDate = DateTime.Now;
                while (true)
                {
                    var newDish = mainApp.ChooseDishes();
                    newOrder.MenuItemWithPrice.AddRange(newDish);

                    var totalPrice = newOrder.MenuItemWithPrice.Select(x => x.Price).Sum();
                    
                    //mainApp.TableInfo.TotalOrderAmount = newOrder.MenuItemWithPrice.Select(x => x.Price).Sum();
                    Console.WriteLine($"Tolal price is : {totalPrice}");
                   
                    if (newDish.Count == 0)
                    {
                        
                        mainApp.OrderInfo = newOrder.MenuItemWithPrice;
                        mainApp.TableInfo = newOrder.Table;
                        mainApp.Data = newOrder.OrderDate;
                        mainApp.ShowOrderDetails();
                        var ReturnOnMenuOrPaid = mainApp.ChooseComandReturnOrPaid();
                        
                        if (ReturnOnMenuOrPaid == 2)
                        {
                            
                            var bill = new BillForUser();
                            bill.BillOrderInfo = newOrder.MenuItemWithPrice;
                            bill.BillData = newOrder.OrderDate;
                            bill.PrintBill();
                            
                            return;
                            
                        }
                    }
                }
            }
            
            
        }
    }
}