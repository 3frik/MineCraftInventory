using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftInventory
{
    internal class Item
    {
        public string Sprite { get; set; }
        public int Ammount;
        public Item()
        {
            Ammount = 1;
            Sprite =
                "00000b00" +
                "00000yy0" +
                "0yyyyyy0" +
                "00yyyy00";

        }

    }

    internal class Weapon : Item
    {
        public string Name;

        public Weapon()
        {
            Ammount = 1;
            Sprite =
                "000bb0bb" +
                "0000wbb0" +
                "00ww00b0" +
                "ww000000";
        }

    }

    internal class Potion : Item
    {
        public Potion()
        {
            Ammount = 1;
            Sprite =
                "00wwww00" +
                "000ww000" +
                "00wrrw00" +
                "0wrrrrw0";
        }
    }
}
