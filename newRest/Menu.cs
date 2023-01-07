using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public class Menu
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public static implicit operator List<object>(Menu v)
        {
            throw new NotImplementedException();
        }
    }
}
