using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewRestoranoSistema.Tests.MainAppTests;

namespace NewRestoranoSistema.Tests
{
    [TestClass]
    public class BillForRestaurantTests
    {
        [TestMethod]
        public void PrintBill_CheckIfRestaurantBillIsPrinted_ReturnSuccessMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            var restaurantBill = new BillForRestaurant(testConsole)
            {
                BillTableInfo = new Table()
                {
                    TableId = 1,
                    TableState = "unavailable",
                    NumberOfSeats = 2,
                },
                BillOrderInfo = new List<Menu>()
                {
                    new Menu
                    {
                        Name = "Pasta",
                        Price = 1,
                    },
                    new Menu
                    {
                        Name = "Tea",
                        Price = 3,
                    }
                },
                BillData = DateTime.Now,
            };
            // Act
            var result = restaurantBill.PrintBill();
            // Assert
            Assert.AreEqual("Payment is successful", result);
        }
       
        [TestMethod]
        public void Save_CheckIfSavedRestaurantBillInFile_ReturnSuccessMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            var restaurantBill = new BillForRestaurant(testConsole)
            {
                BillTableInfo = new Table()
                {
                    TableId = 1,
                    TableState = "unavailable",
                    NumberOfSeats = 2,
                },
                BillOrderInfo = new List<Menu>()
                {
                    new Menu
                    {
                        Name = "Soup",
                        Price = 6,
                    }
                },
                BillData = DateTime.Now,
                _invoice = "E00088",
            };
            // Act
            var result = restaurantBill.Save();
            // Assert
            Assert.AreEqual("Saved in file", result);
        }
            

        [TestMethod]
        public void SendEmail_CheckIfSendRestaurantBill_ReturnSuccessMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            var restaurantBill = new BillForRestaurant(testConsole)
            {
                BillTableInfo = new Table()
                {
                    TableId = 1,
                    TableState = "unavailable",
                    NumberOfSeats = 2,
                },
                BillOrderInfo = new List<Menu>()
                {
                    new Menu
                    {
                        Name = "Soup",
                        Price = 6,
                    }
                },
                BillData = DateTime.Now,
                _invoice = "E00088",
            };
            testConsole.ReadStringResult = "vardas.gmail.com";
            // Act
            var result = restaurantBill.SendEmail();
            // Assert
            Assert.AreEqual("Subject: Restaurant Bill\n Email has been successfully sent to: vardas.gmail.com", result);
        }
       
        public class TestConsole : IConsole
        {
            private int _counter = -1;
            private int _numberCounter = -1;
            public string ReadStringResult { get; set; }
            public int ReadNumberResult { get; set; }
            public string WriteStringResult { get; set; }
            public int WriteNumberResult { get; set; }
            public List<string> ReadStringsList { get; set; } = new List<string>();
            public List<int> ReadNumbersList { get; set; } = new List<int>();

            public string ReadString()
            {
                if (ReadStringResult != null) // kad veiktu pres tai testai
                {
                    return ReadStringResult;
                }
                _counter++;
                return ReadStringsList[_counter];
            }
            public int ReadNumber()
            {
                if (ReadNumberResult != 0)
                {
                    return ReadNumberResult;
                }
                _numberCounter++;
                return ReadNumbersList[_numberCounter];
            }
            public void WriteLine(string value)
            {
                WriteStringResult = value; // tuscias veikia, jeigu nesvarbu ka spausdina console
            }
            public void WriteNumber(int value)
            {
                WriteNumberResult = value;
            }
        }
    }
}
