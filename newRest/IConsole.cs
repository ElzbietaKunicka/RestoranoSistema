using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newRest
{
    public interface IConsole
    {
        public void WriteLine(string value);

        public string ReadLine();
    }
}
