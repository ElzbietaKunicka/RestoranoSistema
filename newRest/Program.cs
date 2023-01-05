using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace NewRestoranoSistema
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var customConsole = new CustomConsole();
                var mainApp = new MainApp(customConsole);
                if (true)
                {
                    var apLog = mainApp.Login();
                    if (apLog == false)
                    {
                        return;
                    }
                }

                //var total = mainApp.CountTotalOrderAmount();

                var newOrder = new Order();
                var table = mainApp.ChooseTable();
                if (table.TableId == 0)
                {
                    return;
                }
                if (table.TableId != 0)
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
                        //Console.WriteLine($"Tolal price is : {totalPrice}eur");
                        if (newDish.Count == 0)
                        {
                            mainApp.OrderInfo = newOrder.MenuItemWithPrice;
                            mainApp.TableInfo = newOrder.Table;
                            mainApp.Data = newOrder.OrderDate;
                            mainApp.ShowOrderDetails();

                            var ReturnOnMenuOrPaid = mainApp.SelectCommandReturnToTheMenuOrToPay();
                            if (ReturnOnMenuOrPaid == 2)
                            {
                                var bill = new BillForUser();
                                bill.BillTableInfo = newOrder.Table;
                                bill.BillOrderInfo = newOrder.MenuItemWithPrice;
                                bill.BillData = newOrder.OrderDate;
                                var ReturnOutputPrintBillOrNoForGuests = mainApp.SelectCommandToPrintBillOrNoForCustomers();
                                var billForRestaurant = new BillForRestaurant();
                                billForRestaurant.BillTableInfo = newOrder.Table;
                                billForRestaurant.BillOrderInfo = newOrder.MenuItemWithPrice;
                                billForRestaurant.BillData = newOrder.OrderDate;
                                if (ReturnOutputPrintBillOrNoForGuests == 1)
                                {
                                    bill.PrintBill();
                                }
                                billForRestaurant.PrintBill();
                                billForRestaurant.Save();
                                mainApp.CheckUnavailableTable();
                                var ta = mainApp.TableInfo.TableState;
                                Console.WriteLine(ta);

                                billForRestaurant.SendEmail(bill);
                                bill.SendEmail(bill);
                               
                                
                                return;
                            }
                        }
                    }
                }
            }

            
           // var customConsole = new CustomConsole();
           // var mainApp = new MainApp(customConsole);
           //if (true)
           // {
           //   var apLog = mainApp.Login();
           //     if(apLog == false)
           //     {
           //         return;
           //     }
           // }
            
           // //var total = mainApp.CountTotalOrderAmount();
           
           // var newOrder = new Order();
           // var table = mainApp.ChooseTable();
           // if(table.TableId == 0)
           // {
           //     return;
           // }
           // if(table.TableId != 0)
           // {
           //     newOrder.Table = table;
           //     //newOrder.TableId = table; change name
           //     newOrder.OrderDate = DateTime.Now;
           //     while (true)
           //     {
           //         var newDish = mainApp.ChooseDishes();
           //         newOrder.MenuItemWithPrice.AddRange(newDish);
           //         var totalPrice = newOrder.MenuItemWithPrice.Select(x => x.Price).Sum();
           //         //mainApp.TableInfo.TotalOrderAmount = newOrder.MenuItemWithPrice.Select(x => x.Price).Sum();
           //         //Console.WriteLine($"Tolal price is : {totalPrice}eur");
           //         if (newDish.Count == 0)
           //         {
           //             mainApp.OrderInfo = newOrder.MenuItemWithPrice;
           //             mainApp.TableInfo = newOrder.Table;
           //             mainApp.Data = newOrder.OrderDate;
           //             mainApp.ShowOrderDetails();
                       
           //             var ReturnOnMenuOrPaid = mainApp.SelectCommandReturnToTheMenuOrToPay();
           //             if (ReturnOnMenuOrPaid == 2)
           //             {
           //                 var bill = new BillForUser();
           //                 bill.BillTableInfo = newOrder.Table;
           //                 bill.BillOrderInfo = newOrder.MenuItemWithPrice;
           //                 bill.BillData = newOrder.OrderDate;
           //                var ReturnOutputPrintBillOrNoForGuests = mainApp.SelectCommandToPrintBillOrNoForCustomers();
           //                 if(ReturnOutputPrintBillOrNoForGuests == 1)
           //                 {
           //                     bill.PrintBill();
           //                 }
                            
           //                 var billForRestaurant = new BillForRestaurant();
           //                 billForRestaurant.BillTableInfo = newOrder.Table;
           //                 billForRestaurant.BillOrderInfo = newOrder.MenuItemWithPrice;
           //                 billForRestaurant.BillData = newOrder.OrderDate;
           //                 //billForRestaurant.PlusInfo();
           //                 billForRestaurant.PrintBill();
           //                 //billForRestaurant.Save();
           //                 return;
           //             }
           //         }
           //     }
           // }

            
        }
        
    }
}