using System;
using static NewRestoranoSistema.Tests.MainAppTests;

namespace NewRestoranoSistema.Tests
{
    [TestClass]
    public class MainAppTests
    {
        [TestMethod]
        public void Login_CheckIfLoginSuccessfullAfterEnterCorrectPin_True()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            var waiter = new Waiter()
            {
                WaiterId = 1,
                Password = "one",
            };
            testConsole.ReadNumberResult = 1;
            testConsole.ReadStringResult = "one";
            // Act
            var result = mainApp.Login();
            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Login_CheckIfSuccessMsgIsReturnedAfterIncorrectPassword_ErrorMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            var waiter = new Waiter()
            {
                WaiterId = 1,
                Password = "one",
            };
            testConsole.ReadNumberResult = 1;
            testConsole.ReadStringResult = "oke";
            // Act
            var result = mainApp.Login();
            // Assert
            Assert.AreEqual("Incorrect WaiterID or password. Try signing in again.", testConsole.WriteStringResult);
        }

        [TestMethod]
        public void ChooseTable_CheckIfSuccessMsgIsReturnedAfterSelectedTableId_SuccessMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole)
            {
            };
            testConsole.ReadNumbersList.Add(4); // num of guests
            testConsole.ReadNumbersList.Add(6); // select table id
            // Act
            var result = mainApp.ChooseTable();
            // Assert
            Assert.AreEqual("Table with ID - 6 has been selected.", testConsole.WriteStringResult);
        }

        [TestMethod]
        public void ChooseTable_CheckIfErrorMsgIsReturnedAfterEnterNumberOfGuestsIsGreaterThanNumberOfSeats_ErrorMessage()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            testConsole.ReadNumbersList.Add(8); // num of guests
            // Act
            var result = mainApp.ChooseTable();
            // Assert
            Assert.AreEqual("Sorry, we don't currently have any tables available for 8", testConsole.WriteStringResult);
        }

        [TestMethod]
        public void ChooseTable_CheckIfTableStateIsChangedAfterChooseTableId_TableStateIsUnavailable()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            testConsole.ReadNumbersList.Add(5); // num of guests
            testConsole.ReadNumbersList.Add(5);
            // Act
            var result = mainApp.ChooseTable();
            // Assert
            Assert.AreEqual("unavailable", result.TableState);
        }

        [TestMethod]
        public void ChooseDishes_CheckIfSuccessMsgIsReturnedAfterSelectedDish_MsgWithDishNameAndPrice()
        {
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            //act
            testConsole.ReadNumberResult = 1; //Dish menu
            testConsole.ReadStringResult = "Pasta";
            var result = mainApp.ChooseDishes();
            //assert
            Assert.AreEqual("Dish has been added. Dish: Pasta,   Price: 5eur", testConsole.WriteStringResult);
        }

        [TestMethod]
        public void ChooseDishes_CheckIfSuccessMsgIsReturnedAfterSelectedDrink_MsgWithDrinkNameAndPrice()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            // Act
            testConsole.ReadNumberResult = 2; //Dish menu
            testConsole.ReadStringResult = "Tea";
            var result = mainApp.ChooseDishes();
            // Assert
            Assert.AreEqual("Drink has been added. Drink: Tea,   Price: 3eur", testConsole.WriteStringResult);
        }
        [TestMethod]
        public void SelectCommandReturnToTheMenuOrToPay_CheckIfNumber2ReturnedAfterSelectedComandToPay_ReturnsSameNumber()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            //Act
            var output = testConsole.ReadNumberResult = 2;
            var result = mainApp.SelectCommandReturnToTheMenuOrToPay();
            //Assert
            Assert.AreEqual(output, result);
        }

        [TestMethod]
        public void ChooseTable_CheckIfTableInfoAreReturnedAfterChooseTableId_ReturnTableInfo()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            testConsole.ReadNumbersList.Add(5); // num of guests
            testConsole.ReadNumbersList.Add(6);
            // Act
            var result = mainApp.ChooseTable();
            //Assert
            Assert.AreEqual(6, result.TableId);
            Assert.AreEqual(6, result.NumberOfSeats);
        }

        [TestMethod]
        public void ChooseDishes_CheckIfDishReturnedAfterChooseDish_ReturnDishNameAndPrice()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            testConsole.ReadNumberResult = 2; //Dish menu
            testConsole.ReadStringResult = "Tea";
            // Act
            var result = mainApp.ChooseDishes();
            // Assert
            Assert.AreEqual("Dish Name: Tea Price: 3", $"Dish Name: {result[0].Name} Price: {result[0].Price}");
        }
        [TestMethod]
        public void UncheckTheUnavailableTable_CheckIfTableStateIsChangedAfterPrintedBill_ReturnTableStateIsAvailable()
        {
            // Arrange
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole)
            {
                TableInfo = new Table()
                {
                    TableId= 1,
                    TableState = "unavailable",
                    NumberOfSeats = 2,
                }
            };

            var result = mainApp.UncheckTheUnavailableTable();
            Assert.AreEqual("available", result);
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
            }
            public void WriteNumber(int value)
            {
                WriteNumberResult = value;
            }
        }
    }
}

