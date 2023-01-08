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

namespace NewRestoranoSistema
{
    public class MainApp 
    {
        private readonly List<Table> _listOftable;
        private readonly List<Menu> _listOfDishes;
        private readonly List<Menu> _listOfDrinks;
        private readonly List<Waiter> _listOfWaiters; // = new List<Waiter>();
        private readonly IConsole _console;
        public Waiter _currentLoggedInWaiter;
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
            var inputId = _console.ReadNumber();
            _currentLoggedInWaiter = _listOfWaiters.SingleOrDefault(waiter => waiter.WaiterId == inputId);
            while (true)
            {
                if (_currentLoggedInWaiter == null)
                {
                    _console.WriteLine("The user ID you entered does not exist. Try again.");
                    while (_currentLoggedInWaiter == null)
                    {
                        _console.WriteLine("Please enter ID");
                        var newInputId = (_console.ReadNumber());
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
                            var password = _console.ReadString();

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
                        _console.WriteLine("Incorrect WaiterID or password. Try signing in again.");
                        return false;
                    }
                }
            }
        }
        public Table ChooseTable()
        {
            _console.WriteLine("Please enter number of guests!");
            var numberOfGuests = _console.ReadNumber();
            var availableTableList = _listOftable.Where(table => table.TableState == "available").ToList();
            var theRightSizeTables = availableTableList.Where(seat => seat.NumberOfSeats >= numberOfGuests).ToList();
            var theRightSizeTablesID = availableTableList.Where(seat => seat.NumberOfSeats >= numberOfGuests).Select(table => table.TableId).ToList();
            if (theRightSizeTablesID.Count == 0)
            {
                var availableTable = new Table();
                _console.WriteLine($"Sorry, we don't currently have any tables available for {numberOfGuests}");
                return availableTable;
            }
                _console.WriteLine("Please select table ID.");
            theRightSizeTablesID.ForEach(_console.WriteNumber);
                int userInputChoice =_console.ReadNumber();
                var table = theRightSizeTables.SingleOrDefault(x => x.TableId == userInputChoice);
            if (table == null)
            {
                while (table == null)
                {
                    _console.WriteLine("Wrong input");
                    _console.WriteLine("Please check your input and select table ID again.");
                    int newUserInputChoice = _console.ReadNumber();
                    table = theRightSizeTables.SingleOrDefault(x => x.TableId == newUserInputChoice);
                    if (table != null)
                    {
                        _console.WriteLine($"Table with ID - {newUserInputChoice} has been selected.");
                        table.TableState = "unavailable";
                        return table;
                    }
                }
            }
            if(table != null && table.TableId == theRightSizeTablesID.SingleOrDefault(tableId => tableId == userInputChoice))
            {
                _console.WriteLine($"Table with ID - {userInputChoice} has been selected.");
                table.TableState = "unavailable";
                return table;
            }
            return table;
        }
        public List<Menu> ChooseDishes()
        {
            while (true)
            {
                _console.WriteLine("Select the menu of dishes or Drinks");
                _console.WriteLine("1- dishes menu");
                _console.WriteLine("2- drinks menu");
                _console.WriteLine("3- Show order details");
                var drinkList = new List<Menu>();
                var dishesList = new List<Menu>();
                var menuInputChoice = _console.ReadNumber();
                
                switch (menuInputChoice)
                {
                    case 1:
                        var dishesNames = _listOfDishes.Select(dishes => dishes.Name).ToList();
                        _console.WriteLine("Please select dish");
                        dishesNames.ForEach(_console.WriteLine);
                        var inputDish = _console.ReadString();
                        var dish = _listOfDishes.SingleOrDefault(dish => dish.Name == inputDish);
                        while (true)
                        {
                            if (dish == null)
                            {
                                _console.WriteLine("The dish does not exist.");
                                while (dish == null)
                                {
                                    _console.WriteLine("Please enter dish Name again");
                                    var newInputDish = (_console.ReadString());
                                    dish = _listOfDishes.SingleOrDefault(waiter => waiter.Name == newInputDish);
                                }
                            }
                            if (dish != null)
                            {
                                _console.WriteLine($"Dish has been added. Dish: {dish.Name},   Price: {dish.Price}eur");
                                dishesList.Add(dish);
                                return dishesList;
                            }
                        }
                        break;
                       
                    case 2:
                         var drinksNames = _listOfDrinks.Select(drink => drink.Name).ToList();
                        _console.WriteLine("Please select drink");
                        drinksNames.ForEach(_console.WriteLine);
                        var inputDrinkName = _console.ReadString();
                        var drink = _listOfDrinks.SingleOrDefault(drink => drink.Name == inputDrinkName);
                        while (true)
                        {
                            if (drink == null)
                            {
                                _console.WriteLine("Drink doesn't exist, PLEASE CHECK AND TRY AGAIN.");
                                while (drink == null)
                                {
                                    _console.WriteLine("Please enter drink name again");
                                    var newInputDrinkName = (_console.ReadString());
                                    drink = _listOfDrinks.SingleOrDefault(drink => drink.Name == newInputDrinkName);
                                }
                            }
                            if (drink != null)
                            {
                                _console.WriteLine($"Drink has been added. Drink: {drink.Name},   Price: {drink.Price}eur");
                                drinkList.Add(drink);
                                return drinkList;
                            }
                        }
                        break;

                    case 3:
                        var result = new List<Menu>();
                        return result;
                        
                    default:
                         _console.WriteLine("PLEASE CHECK AND TRY AGAIN.");
                         break;
                }
            }
        }
        public void ShowOrderDetails()
        {
            _console.WriteLine($" \n Order details:");
            var items = OrderInfo;
            var counter = 1;
            var total = items.Select(item => item.Price).Sum();
           _console.WriteLine($" Table ID: {TableInfo.TableId}, \n NumberOfSeats: {TableInfo.NumberOfSeats} seats.");
            foreach (var item in items)
            {
                _console.WriteLine($"\t {counter++}.{item.Name}  \t{item.Price}eur.");
            }
            _console.WriteLine($"The order total amount = {total} eur.");
            _console.WriteLine($"Order date: {Data} \n");
            return;
        }

        public int SelectCommandReturnToTheMenuOrToPay()
        {
            _console.WriteLine("Please select command: \n 1 - Back to the main menu \n 2 - To pay.");
            var output = _console.ReadNumber();
            if (output == 1)
            {
                var newDish = ChooseDishes();
                OrderInfo.AddRange(newDish);
                return output;
            }
            if (output == 2)
            {
                return output;
            }
            return output;
        }

        public int SelectCommandToPrintBillOrNoForCustomers()
        {
            _console.WriteLine("Select command:\n 1.Print a customer and restaurant bills \n 2.Print a Restaurant bill ");
            var output = _console.ReadNumber();
            if (output != 1 && output != 2)
            {
                _console.WriteLine("Wrong input, try again");
                output = _console.ReadNumber();
                
                if (output == 1 || output == 2)
                {
                    return output;
                }
            }
            if (output == 1 || output == 2)
            {
               return output;
            }
            return output;
        }
        public string UncheckTheUnavailableTable()
        {
           TableInfo.TableState = "available";
            _console.WriteLine($"Table ID - {TableInfo.TableId} is available!!!\n");
           return "available";
        }
        public int SelectCommandToSendEmailsOrNo()
        {
            _console.WriteLine("Would you like to send a Billcopy? \n 1.YES \n 2.NO");
            var output = _console.ReadNumber();
            if (output != 1 && output != 2)
            {
                _console.WriteLine("Wrong input, try again");
                output = _console.ReadNumber();
                if (output == 1 || output == 2)
                {
                    return output;
                }
            }
            if (output == 1 || output == 2)
            {
                return output;
            }
            return output;
        }
        public int SelectTheBIllThatYouWantToSend()
        {
            _console.WriteLine("Select command \n 1.Send the Restaurant bill. \n 2.Send the Customer Bill \n 3.Send copies of both bills \n 4. Return");
            var output = _console.ReadNumber();
            if (output != 1 && output != 2 && output != 3 && output != 4)
            {
                _console.WriteLine("Wrong input, try again");
                output = _console.ReadNumber();
                if (output == 1 || output == 2 || output == 3 || output == 4)
                {
                    return output;
                }
            }
            if (output == 1 || output == 2 || output == 3 || output == 4)
            {
                return output;
            }
            return output;
        }
    }
}


        
        
