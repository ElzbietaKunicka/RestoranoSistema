using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newRest
{
    public class CustomConsole : IConsole
    {
        public string ReadLine() => Console.ReadLine();
        
        public void WriteLine(string value) => Console.WriteLine(value);
        
    }
}
