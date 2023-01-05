using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public interface IConsole
    {
        public void WriteLine(string value);
        public void WriteNumber(int value);

        public string ReadString();
        public int ReadNumber();
    }
}
