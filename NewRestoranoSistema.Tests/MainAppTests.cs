using System;
using static NewRestoranoSistema.Tests.MainAppTests;

namespace NewRestoranoSistema.Tests
{
    [TestClass]
    public class MainAppTests
    {
        [TestMethod]
        public void Login_CheckIfLoginSuccessfullAfterCorrectPin_True()
        {
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole)
            {
            };
            var waiter = new Waiter()
            {
                WaiterId = 1,
                Password = "one",
            };
            //act
            testConsole.ReadNumberResult = 1;
            testConsole.ReadStringResult = "one";
            var result = mainApp.Login();
            //assert
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void Login_CheckIfSuccessMsgIsReturnedAfterIncorrectPassword_ErrorMessage()
        {
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole)
            {
            };
            var waiter = new Waiter()
            {
                WaiterId = 1,
                Password = "one",
            };
            testConsole.ReadNumberResult = 1;
            testConsole.ReadStringResult = "oke";
            var result = mainApp.Login();
            //assert
            Assert.AreEqual("Incorrect WaiterID or password. Try signing in again.", testConsole.WriteStringResult);
        }

        [TestMethod]
        public void ChooseTable_CheckIfSuccessMsgIsReturnedAfterSelectedTableId_SuccessMessage()
        {
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole)
            {
            };
            testConsole.ReadNumbersList.Add(4); // num of guests
            testConsole.ReadNumbersList.Add(6); // select table id
            var result = mainApp.ChooseTable();
            //Assert
            Assert.AreEqual("Table with ID - 6 has been selected.", testConsole.WriteStringResult);
        }

        [TestMethod]
        public void ChooseTable_CheckIfErrorMsgIsReturnedAfterEnterNumberOfGuestsIsGreaterThanNumberOfSeats_ErrorMessage()
        {
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);

           //act
            testConsole.ReadNumbersList.Add(8); // num of guests
            var result = mainApp.ChooseTable();
            
            //assert
            Assert.AreEqual("Sorry, we don't currently have any tables available for 8", testConsole.WriteStringResult);
        }

        [TestMethod]
        public void ChooseTable_CheckIfTableStateIsChangedAfterChooseTableId_TableStateIsUnavailable()
        {
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            
            //act
            testConsole.ReadNumbersList.Add(5); // num of guests
            testConsole.ReadNumbersList.Add(5);
            var result = mainApp.ChooseTable();

            //assert
            //Assert.AreEqual("available", table.TableState);
            Assert.AreEqual("unavailable", result.TableState);
        }

        [TestMethod]
        public void ChooseDishes_CheckIfSelectedDishAfterReturnDishWithNameAndPrice_MsgWithDishNameAndPrice()
        {
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            //act
            testConsole.ReadNumberResult = 1; //Dish menu
            testConsole.ReadStringResult = "Pasta"; 
            //testConsole.ReadStringsList.Add("Soup");
            var result = mainApp.ChooseDishes();
            //assert
            Assert.AreEqual("Dish has been added. Dish: Pasta,   Price: 5eur", testConsole.WriteStringResult);
        }

        [TestMethod]
        public void ChooseDishes_CheckIfSelectedDrinkAfterReturnDrinkWithNameAndPrice_MsgWithDrinkNameAndPrice()
        {
            var testConsole = new TestConsole();
            var mainApp = new MainApp(testConsole);
            //act
            testConsole.ReadNumberResult = 2; //Dish menu
            testConsole.ReadStringResult = "Tea";
            var result = mainApp.ChooseDishes();
            //assert
            Assert.AreEqual("Drink has been added. Drink: Tea,   Price: 3eur", testConsole.WriteStringResult);
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