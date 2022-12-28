using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace newRest
{
    public class MainApp
    {
        private readonly IConsole _console;
        private readonly List<Waiter> _list = new List<Waiter>();
        public Waiter _currentLoggedInWaiter;
        static List<Waiter> ReadWaitersData(string path)

        {
            string fileContent = File.ReadAllText(path);
            var waitersList = JsonSerializer.Deserialize<List<Waiter>>(fileContent);
            return waitersList;
        }

        static List<Table> ReadTablesData(string path)
        {
            string fileContent = File.ReadAllText(path);
            var tablesList = JsonSerializer.Deserialize<List<Table>>(fileContent);
            return tablesList;
        }
        static List<Menu> ReadMenuData(string path)
        {
            string fileContent = File.ReadAllText(path);
            var dishesList = JsonSerializer.Deserialize<List<Menu>>(fileContent);
            return dishesList;
        }
        public MainApp(IConsole console)
        {
            _console = console;
        }

        public bool Login()
        {
            var result = ReadWaitersData("./Waiters.json");
            _console.WriteLine("Prasau ivesti ID.");
            var inputId = int.Parse(_console.ReadLine());
            _currentLoggedInWaiter = result.First(waiter => waiter.WaiterId == inputId);

            if(_currentLoggedInWaiter == null)
            {
                _console.WriteLine("Netinkamas id");
                return false;
            }
            _console.WriteLine("Prasau ivesti slaptazodi");
            for (int i = 0; i < 3; i++)
            {
                var password = _console.ReadLine();

                if (_currentLoggedInWaiter.Password == password)
                {
                    _console.WriteLine($"your ID is - {_currentLoggedInWaiter.WaiterId}");
                    return true;
                }
                if (i != 2)
                {
                    _console.WriteLine($"Blogas slaptazodis! Turite {2 - i} meginimus");
                }
            }
            _console.WriteLine($"Blogas slaptazodis!");

            return false;

        }

        public bool ChooseTable()
        {
            var listOfTable = ReadTablesData("./Tables.json");
            _console.WriteLine("Norint pasirinkti staliuka, iveskite zmoniu skaiciu? ");
            var gues = int.Parse(_console.ReadLine());
            var availableTable = listOfTable.Where(table => table.TableState == "available").ToList();

            var tinkamasStaliukasID = availableTable.Where(seat => seat.NumberOfSeats >= gues).Select(Table => Table.TableId).ToList();
            
            
            if (tinkamasStaliukasID.Count == 0)
            {
                _console.WriteLine("atsiprasome, visi staliukai uzimti");
                return true;
            };
            _console.WriteLine("pasirinkite staliuko Id");
            tinkamasStaliukasID.ForEach(Console.WriteLine);
            _console.WriteLine("test");
           
           
            while (true)
            {
                var userInputChoice = int.Parse(Console.ReadLine());

                switch(userInputChoice) 
                {
                    case 6:
                        _console.WriteLine("pasirinktas staliukas, kuriuo Id 6,galite kurti uzsakyma");
                        var orderResultfor6table = CreateOrder(userInputChoice);
                        break;
                    case 5:
                        _console.WriteLine("pasirinktas staliukas, kuriuo Id 5, galite kurti uzsakyma");
                        var orderResultfor5 = CreateOrder(userInputChoice);
                        break;
                    case 4:
                        var table4 = listOfTable.Single(x => x.TableId == 4);
                        table4.TableState = "unavailbe";
                        _console.WriteLine($"pasirinktas staliukas, kuriuo Id {table4.TableId}, galite kurti uzsakyma");
                        var orderResultfor4 = CreateOrder(userInputChoice);
                        break;
                }
            }
           
            return true;
        }

        public List<Menu> CreateOrder(int tableid)
        {
            //_console.WriteLine("pasirinkite norima menu:");
            //_console.WriteLine("1- dishes");
            //_console.WriteLine("2- drinks");
            //_console.WriteLine("3- save");

            var listOfDishes = ReadMenuData("./Dishes.json");

            var newOr = new List<string>();

            while (true)
            {
                _console.WriteLine("pasirinkite norima menu:");
                _console.WriteLine("1- dishes");
                _console.WriteLine("2- drinks");
                _console.WriteLine("3- save");
                var menuInputChoice = int.Parse(Console.ReadLine());
                
                switch (menuInputChoice)
                {
                    case 1:
                        var dishesName = listOfDishes.Select(dishes => dishes.Name).ToList();
                        _console.WriteLine("pasirinkitie patiekala");
                        dishesName.ForEach(Console.WriteLine);
                        var inputDishes = (_console.ReadLine()).ToString();
                        var item = listOfDishes.Where(d => d.Name == inputDishes).ToList();
                        var dishesPrice = listOfDishes.Select(dishes => dishes.Price).ToList();

                        var itemName = item.Select(d => d.Name).ToList();
                        //itemName.ForEach(Console.WriteLine);
                        var itemPrice = item.Select(d => d.Price).ToList();
                        //itemPrice.ForEach(Console.WriteLine);

                        if (item == null)
                        {
                           _console.WriteLine( "not exist dishes");
                        }else
                        {
                            
                            
                            newOr.Add($"Name: {itemName[0]} Price:{itemPrice[0]}");
                            _console.WriteLine($"idetas i uzsakyma: Dishes: {itemName[0]},   Price: {itemPrice[0]} eur");

                        }

                        //while (true)
                        //{
                        //    var chooseDishes = (_console.ReadLine()).ToString();
                        //    switch (chooseDishes)
                        //    {
                        //        case "Pasta":

                        //            var item = listOfDishes.Where(item => item.DishesName == chooseDishes).ToList();
                        //            var pastaName = item.Select(x => x.DishesName).ToList();
                        //            var pastaPrice = item.Select(x => x.Price).ToList();

                        //            var name = pastaName[0];
                        //            var price = pastaPrice[0];
                        //            order.Add($"{name}, {price}");

                        //            _console.WriteLine($"idetas i uzsakyma: Dishes: {name},   Price: {price} eur");


                        //            break;
                        //        default:
                        //            _console.WriteLine("wrong input");
                        //            break;

                        //    } 
                        //}
                        
                        break;
                    case 2:
                        _console.WriteLine("pasirinkitie patiekala");
                        break;
                        case 3:
                        _console.WriteLine("logout");
                        break;
                    default:
                         _console.WriteLine("wrong input");
                         break;

                }

            }

            
        }

    }
}


        
        
