using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public class CustomConsole : IConsole
    {
        public string ReadString() => Console.ReadLine();

        public int ReadNumber()
        {
            var userInput = Console.ReadLine();
            int integerResult;
            bool parsedSuccessuly = int.TryParse(userInput, out integerResult);

            while (parsedSuccessuly == false) 
            {
                Console.WriteLine("Please input numbers only! ");
                userInput = Console.ReadLine();
                parsedSuccessuly = int.TryParse(userInput, out integerResult);
            }
            return integerResult;
        }

        public void WriteLine(string value) => Console.WriteLine(value);

        public void WriteNumber(int value) => Console.WriteLine(value);

        //public void WriteLine1(int value) => Console.WriteLine(value);
    }
}
