using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace newRest
{
    public class MainApp 
    {
        private readonly List<Table> _listOftable;
        private readonly List<Menu> _listOfDishes;
        private readonly List<Menu> _listOfDrinks;


        private readonly IConsole _console;
        private readonly List<Waiter> _list = new List<Waiter>();
        public Waiter _currentLoggedInWaiter;

        public MainApp(IConsole console)
        {
            _console = console;
            _listOftable = ReadTablesData("./Tables.json");
            _listOfDishes = ReadMenuData("./Dishes.json");
            _listOfDrinks = ReadMenuData("./Drinks.json");
        }

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


        public bool Login()
        {
            var result = ReadWaitersData("./Waiters.json");
            _console.WriteLine("Prasau ivesti ID.");
            var inputId = int.Parse(_console.ReadLine());
            _currentLoggedInWaiter = result.SingleOrDefault(waiter => waiter.WaiterId == inputId);

           
                //if (_currentLoggedInWaiter == null)
                //{
                //    _console.WriteLine("Netinkamas id");
                    
                //}
            
                
                
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
            
               //_console.WriteLine($"Blogas slaptazodis!");
            

            
             
            //if(_currentLoggedInWaiter == null)
            //{
            //    _console.WriteLine("Netinkamas id");
            //    return false;
            //}
            //_console.WriteLine("Prasau ivesti slaptazodi");
            //for (int i = 0; i < 3; i++)
            //{
            //    var password = _console.ReadLine();

            //    if (_currentLoggedInWaiter.Password == password)
            //    {
            //        _console.WriteLine($"your ID is - {_currentLoggedInWaiter.WaiterId}");
            //        return true;
            //    }
            //    if (i != 2)
            //    {
            //        _console.WriteLine($"Blogas slaptazodis! Turite {2 - i} meginimus");
            //    }
            //}
            //_console.WriteLine($"Blogas slaptazodis!");
            //return true;
        }
      
        // metodas turi leisti issirinkti staliuka ir ji grazinti. patiekalu uzsakymo ir kitu dalyku cia neturetu vykti
        public Table ChooseTable()
        {
            _console.WriteLine("Norint pasirinkti staliuka, iveskite zmoniu skaiciu? ");
            var gues = int.Parse(_console.ReadLine());
            var availableTable = _listOftable.Where(table => table.TableState == "available").ToList();
            var tinkamasStaliukasID = availableTable.Where(seat => seat.NumberOfSeats >= gues).Select(Table => Table.TableId).ToList();
            //var isrinktasStaliukas = 0;
            
            if (tinkamasStaliukasID.Count == 0)
            {
                _console.WriteLine("atsiprasome, visi staliukai uzimti");
               return null;
            };
            _console.WriteLine("please choose TableId");
            tinkamasStaliukasID.ForEach(Console.WriteLine);
            _console.WriteLine("test");
            var userInputChoice = int.Parse(Console.ReadLine());
            
            var table = _listOftable.Single(x => x.TableId == userInputChoice);
            _console.WriteLine($"pasirinkto staliuko ID is {userInputChoice}");
            table.TableState = "unavailbe";

            return table;
        }
         
        public List<Menu> ChooseDishes()
        {
            
            //var listOfDishes = ReadMenuData("./Dishes.json");
            while (true)
            {
                _console.WriteLine("Select the menu of dishes or Drinks");
                _console.WriteLine("1- dishes menu");
                _console.WriteLine("2- drinks menu");
                _console.WriteLine("3- return");

                var menuInputChoice = int.Parse(_console.ReadLine());
                
                switch (menuInputChoice)
                {
                    case 1:
                        var dishesList = new List<Menu>();
                        var dishesNames = _listOfDishes.Select(dishes => dishes.Name).ToList();
                        _console.WriteLine("Please select dish");
                        dishesNames.ForEach(Console.WriteLine);
                        var inputDish = (_console.ReadLine()).ToString();
                        var dish = _listOfDishes.Single(dish => dish.Name == inputDish);
                        dishesList.Add(dish);

                        if (dish == null)
                        {
                            _console.WriteLine("Dish doesn't exist, PLEASE CHECK AND TRY AGAIN.");
                            break;
                        } 
                        if(dish != null) 
                        {
                            //var dish = _listOfDishes.Single(x => x.Name == inputDishes);
                           _console.WriteLine($"Dish has been added. Dish: {dish.Name},   Price: {dish.Price} eur");
                           return dishesList;
                        }
                        break;
                    case 2:
                        var drinkList = new List<Menu>();
                         var drinksNames = _listOfDrinks.Select(drink => drink.Name).ToList();
                        _console.WriteLine("Please select drink");
                        drinksNames.ForEach(Console.WriteLine);
                        var inputDrink = (_console.ReadLine()).ToString();
                        var drink = _listOfDrinks.Single(drink => drink.Name == inputDrink);
                        drinkList.Add(drink);
                        if (drink == null)
                        {
                            _console.WriteLine("Drink doesn't exist, PLEASE CHECK AND TRY AGAIN.");
                            break;
                        }
                        if (drink != null)
                        {
                            //var drink = _listOfDishes.Single(x => x.Name == inputDishes);
                            _console.WriteLine($"Drink has been added. Drink: {drink.Name},   Price: {drink.Price} eur");
                            return drinkList;
                        }
                        
                        break;
                    case 3:
                    
                       return new List<Menu>();
                        
                    default:
                         _console.WriteLine("PLEASE CHECK AND TRY AGAIN.");
                         break;
                }
            return new List<Menu>();
            }
        }

        public int CountTotalOrderAmount()
        {
            //var newOrder = new Order();
            //var table = mainApp.ChooseTable();
            //newOrder.TableId = table;

            var total = new Menu();
            var ChooseDishesPrice = ChooseDishes();
            var inttt = total.Price;
            var sum = inttt;

            var prices = _listOfDrinks.Select(drink => drink.Name).ToList();
            return sum;
        }
        


    }
}


        
        
