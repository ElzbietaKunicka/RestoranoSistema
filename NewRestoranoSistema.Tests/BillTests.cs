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
    public class BillTests
    {
        [TestMethod]
        public void Print_CheckIfRestaurantBillIsPrinted_ReturnSuccessMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            //var mainApp = new MainApp(testConsole);
            var restaurantBill = new BillForRestaurant(testConsole)
            {
                BillTableInfo = new Table()
                {
                    TableId = 1,
                    TableState = "available",
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
            //act
            var result = restaurantBill.PrintBill();
            //Assert
            Assert.AreEqual("Payment is successful", result);
        }
        [TestMethod]
        public void Print_CheckIfCustomerBillIsPrinted_ReturnSuccessMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            var customerBill = new BillForCustomer(testConsole)
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
                    },
                    new Menu
                    {
                        Name = "Cola",
                        Price = 3,
                    }
                },
                BillData = DateTime.Now,
            };
            //act
            var result = customerBill.PrintBill();
            //Assert
            Assert.AreEqual("Thank you, have a nice day.", result);
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
            //act
                var result =  restaurantBill.Save();
                //Assert
                Assert.AreEqual("Save in file", result);
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
            //act
            var result = restaurantBill.SendEmail();
            //Assert
            Assert.AreEqual("Subject: Restaurant Bill\n \tEmail has been successfully sent to: vardas.gmail.com", result);
        }
        [TestMethod]
        public void SendEmail_CheckIfSendCustomerBill_ReturnSuccessMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            var customerBill = new BillForCustomer(testConsole)
            {
                BillTableInfo = new Table()
                {
                    TableId = 1,
                    TableState = "unavailable",
                    NumberOfSeats = 4,
                },
                BillOrderInfo = new List<Menu>()
                {
                    new Menu
                    {
                        Name = "Pizza",
                        Price = 6,
                    }
                },
                BillData = DateTime.Now,
            };
            testConsole.ReadStringResult = "vardas.gmail.com";
            //act
            var result = customerBill.SendEmail();
            //Assert
            Assert.AreEqual("...Email has been successfully sent to: vardas.gmail.com", result);
        }





        public class TestConsole : IConsole
        {
            public string ReadStringResult { get; set; }
            public int ReadNumberResult { get; set; }
            public string WriteStringResult { get; set; }
            public int WriteNumberResult { get; set; }

            public List<string> ReadStringsList { get; set; } = new List<string>();

            public List<int> ReadNumbersList { get; set; } = new List<int>();
            private int _counter = -1;
            private int _numberCounter = -1;

            public string ReadString()
            {
                if (ReadStringResult != null) // is not null buvo// kad veiktu pres tai testai
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
                WriteStringResult = value; // tuscias veikia, jei nesvarbu ka consolei
                //Output.Add(value);
            }
            public void WriteNumber(int value)
            {
                WriteNumberResult = value;
            }
        }
    }


}
