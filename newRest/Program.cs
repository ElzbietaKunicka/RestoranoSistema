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
                    var appLogIn = mainApp.Login();
                    if (appLogIn == false)
                    {
                        return;
                    }
                }
                var newOrder = new Order();
                var table = mainApp.ChooseTable();
                if (table.TableId == 0)
                {
                    return;
                }
                if (table.TableId != 0)
                {
                    newOrder.Table = table;
                    newOrder.OrderDate = DateTime.Now;
                    while (true)
                    {
                        var newDish = mainApp.ChooseDishes();
                        newOrder.MenuItemWithPrice.AddRange(newDish);
                        var totalPrice = newOrder.MenuItemWithPrice.Select(x => x.Price).Sum();
                        if (newDish.Count == 0)
                        {
                            mainApp.OrderInfo = newOrder.MenuItemWithPrice;
                            mainApp.TableInfo = newOrder.Table;
                            mainApp.Data = newOrder.OrderDate;
                            mainApp.ShowOrderDetails();
                            var returnOnMenuOrPaid = mainApp.SelectCommandReturnToTheMenuOrToPay();
                            if (returnOnMenuOrPaid == 2) // 2 - To pay.
                            {
                                var billForCustomer = new BillForCustomer(customConsole);
                                billForCustomer.BillTableInfo = newOrder.Table;
                                billForCustomer.BillOrderInfo = newOrder.MenuItemWithPrice;
                                billForCustomer.BillData = newOrder.OrderDate;
                                var returnOutputPrintBillOrNoForCustomer = mainApp.SelectCommandToPrintBillOrNoForCustomers();
                                var billForRestaurant = new BillForRestaurant(customConsole);
                                billForRestaurant.BillTableInfo = newOrder.Table;
                                billForRestaurant.BillOrderInfo = newOrder.MenuItemWithPrice;
                                billForRestaurant.BillData = newOrder.OrderDate;
                                if (returnOutputPrintBillOrNoForCustomer == 1)
                                {
                                    billForCustomer.PrintBill();
                                }
                                billForRestaurant.PrintBill();
                                billForRestaurant.Save();
                                mainApp.UncheckTheUnavailableTable();
                                var returnOutputOrSendEmail = mainApp.SelectCommandToSendEmailsOrNo();
                                if(returnOutputOrSendEmail == 1)
                                {
                                    var outputThatBillYouWantToSend = mainApp.SelectTheBIllThatYouWantToSend();
                                    if(outputThatBillYouWantToSend == 1)
                                    {
                                        billForRestaurant.SendEmail();
                                    }
                                    if(outputThatBillYouWantToSend == 2)
                                    {
                                        billForCustomer.SendEmail();
                                    }
                                    if(outputThatBillYouWantToSend == 3)
                                    {
                                        billForRestaurant.SendEmail();
                                        billForCustomer.SendEmail();
                                    }
                                    if (outputThatBillYouWantToSend == 4)
                                    {
                                        return;
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
            }

          
            
        }
    }
}