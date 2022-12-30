using System.Runtime.CompilerServices;

namespace newRest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var customConsole = new CustomConsole();
            var mainApp = new MainApp(customConsole);
            //while (true)
            //{
            //   mainApp.Login();
              
            //}
            
            //var total = mainApp.CountTotalOrderAmount();
            mainApp.Login();


            var newOrder = new Order();
            var table = mainApp.ChooseTable();
            newOrder.TableId = table;
            //var newDish = mainApp.ChooseDishes();
            //newOrder.ItemWithPrice = newDish;
            
            while (true)
            {
                var newDish = mainApp.ChooseDishes();
                newOrder.MenuItemWithPrice.AddRange(newDish);
                if(newDish.Count == 0)
                {
                    return;
                }
                //mainApp.ChooseDishes();
            }
            //var total = mainApp.CountTotalOrderAmount();
            //newOrder.TotalOrderAmount = total;


            //newDish.ItemWithPrice = dish;

            
            



        }

    }
}