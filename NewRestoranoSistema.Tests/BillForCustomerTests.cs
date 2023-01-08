using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema.Tests
{
    [TestClass]
    public class BillForCustomerTests
    {
        [TestMethod]
        public void PrintBill_CheckIfCustomerBillIsPrinted_ReturnSuccessMessage()
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
            // Act
            var result = customerBill.PrintBill();
            // Assert
            Assert.AreEqual("Thank you, have a nice day.", result);
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
            // Act
            var result = customerBill.SendEmail();
            // Assert
            Assert.AreEqual("Email has been successfully sent to: vardas.gmail.com", result);
        }
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
            WriteStringResult = value; // tuscias veikia
        }
        public void WriteNumber(int value)
        {
            WriteNumberResult = value;
        }
    }
}
