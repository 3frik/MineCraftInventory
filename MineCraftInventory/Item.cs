using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftInventory
{
    internal class Item
    {
        private string sprite;  //9*5, info for 45 bits of color
        public string Sprite { get { return sprite; } set { sprite = value; } }
        private int ammount;
        public int Ammount { get { return ammount; } set { ammount = value; } }
        public Item()
        {
            ammount = 0;
            sprite =
                "00000y00" +
                "00000yy0" +
                "0yyyyyy0" +
                "00yyyy00" ;

        }

    }
}
