using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        private readonly List<Waiter> _listOfWaiters = new List<Waiter>();

        private readonly IConsole _console;
       
        public Waiter _currentLoggedInWaiter;

        //public  List<Menu> FullOrder { get; set; }
        //public int Total { get; set; }
       
        //public int NumberOfSeats { get; set; }
        public List<Menu> OrderInfo { get; set; }
        public Table TableInfo { get; set; }
        public DateTime Data { get; set; }

        public MainApp(IConsole console)
        {
            _console = console;
            _listOftable = ReadTablesData("./Tables.json");
            _listOfDishes = ReadMenuData("./Dishes.json");
            _listOfDrinks = ReadMenuData("./Drinks.json");
            _listOfWaiters = ReadWaitersData("./Waiters.json");
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
            _console.WriteLine("Please enter ID");
            var inputId = int.Parse(_console.ReadLine());
            _currentLoggedInWaiter = _listOfWaiters.SingleOrDefault(waiter => waiter.WaiterId == inputId);
            while (true)
            {
                if (_currentLoggedInWaiter == null)
                {
                    _console.WriteLine("The user ID you entered does not exist. Try again.");
                    while (_currentLoggedInWaiter == null)
                    {
                        _console.WriteLine("Please enter ID");
                        var newInputId = int.Parse(_console.ReadLine());
                        _currentLoggedInWaiter = _listOfWaiters.SingleOrDefault(waiter => waiter.WaiterId == newInputId);
                    }
                }
                if (_currentLoggedInWaiter != null)
                {
                    _console.WriteLine($"Please enter Password, your ID is {_currentLoggedInWaiter.WaiterId}");
                    while (_currentLoggedInWaiter != null)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            var password = _console.ReadLine();

                            if (_currentLoggedInWaiter.Password == password)
                            {
                                _console.WriteLine($"You are logged in as user with Id {_currentLoggedInWaiter.WaiterId}.");
                                return true;
                            }
                            if (i != 2)
                            {
                                _console.WriteLine($"Incorrect password! You have {2 - i} attempts left");
                            }
                        }
                        _console.WriteLine($"Incorrect WaiterID or password. Try signing in again.");
                        return false;
                    }
                }
            }
        }
        // metodas turi leisti issirinkti staliuka ir ji grazinti. patiekalu uzsakymo ir kitu dalyku cia neturetu vykti
        public Table ChooseTable()
        {
            _console.WriteLine("Please enter number of guests!");
            var numberOfGuests = int.Parse(_console.ReadLine());
            var availableTable = _listOftable.Where(table => table.TableState == "available").ToList();
            var tinkamasStaliukas = availableTable.Where(seat => seat.NumberOfSeats >= numberOfGuests).ToList();
            var tinkamasStaliukasID = availableTable.Where(seat => seat.NumberOfSeats >= numberOfGuests).Select(table => table.TableId).ToList();
            //var isrinktasStaliukas = 0;
            if (tinkamasStaliukasID.Count == 0)
            {
                var newTest = new Table();
                _console.WriteLine($"Sorry, we don't currently have any tables available for {numberOfGuests}");
                return newTest;
            }
                _console.WriteLine("Please select table ID.");
               tinkamasStaliukasID.ForEach(Console.WriteLine);
                _console.WriteLine("  ");
                int userInputChoice = int.Parse(_console.ReadLine());
                var table = tinkamasStaliukas.SingleOrDefault(x => x.TableId == userInputChoice);
            if (table == null)
            {
                //_console.WriteLine("Wrong input");
                while (table == null)
                {
                    _console.WriteLine("Wrong input");
                    _console.WriteLine("Please check your input and select table ID again.");
                    int newUserInputChoice = int.Parse(_console.ReadLine());
                    table = tinkamasStaliukas.SingleOrDefault(x => x.TableId == newUserInputChoice);
                    if (table != null)
                    {
                        _console.WriteLine($"Table with ID - {newUserInputChoice} has been selected.");
                        table.TableState = "unavailable";
                        return table;
                    }
                }
                //return table;
            }
            if(table != null && table.TableId == tinkamasStaliukasID.SingleOrDefault(tableId => tableId == userInputChoice))
            {
                _console.WriteLine($"Table with ID - {userInputChoice} has been selected.");
                table.TableState = "unavailable";
                return table;
            }
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
                _console.WriteLine("3- See order details");

                var drinkList = new List<Menu>();
                var dishesList = new List<Menu>();

                var menuInputChoice = int.Parse(_console.ReadLine());
                
                switch (menuInputChoice)
                {
                    case 1:
                        //var dishesList = new List<Menu>();
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
                           _console.WriteLine($"Dish has been added. Dish: {dish.Name},   Price: {dish.Price}eur");
                           return dishesList;
                        }
                        break;
                    case 2:
                        //var drinkList = new List<Menu>();
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
                            _console.WriteLine($"Drink has been added. Drink: {drink.Name},   Price: {drink.Price}eur");
                            
                            return drinkList;
                        }
                        break;
                    case 3:
                        var result = new List<Menu>();
                        return result;
                        
                    default:
                         _console.WriteLine("PLEASE CHECK AND TRY AGAIN.");
                         break;
                }
            //return new List<Menu>();
            }
        }
        public void ShowOrderDetails()
        {
            _console.WriteLine("");
            Console.WriteLine($"Order details:");
            var items = OrderInfo;
            var counter = 1;
            var total = items.Select(item => item.Price).Sum();
            _console.WriteLine(" ");
           _console.WriteLine($" Table ID: {TableInfo.TableId}, NumberOfSeats: {TableInfo.NumberOfSeats} seats.");
            _console.WriteLine(" ");
            foreach (var item in items)
            {
                _console.WriteLine($"   {counter++}.{item.Name}   {item.Price}eur.");

            }
            _console.WriteLine($"The order total amount is {total} eur.");
            _console.WriteLine("");
            _console.WriteLine($"Order date: {Data}");

            return;
        }

        public int ChooseComandReturnOrPaid()
        {
            Console.WriteLine(" 1 - Back to the main menu");
            Console.WriteLine(" 2 - To pay.");

            var output = int.Parse(_console.ReadLine());
            if (output == 1)
            {
                var newDish = ChooseDishes();
                OrderInfo.AddRange(newDish);
                return output;
            }
            if (output == 2)
            {
                _console.WriteLine("Apmokejimui reikalingas bill");
                return output;
            }
            return output;
            
        }

        

        
    }
}


        
        
