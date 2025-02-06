using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftInventory
{
    internal class Inventory
    {
        private Item[] items=new Item[27];
        private Interface iface = new Interface();

        public Inventory() 
        { 
            items[0] = new Item();
            items[0].Ammount = 13;
            items[1] = new Item();
            items[1].Ammount = 37;
        }

        public void Run()
        {
            bool inventorying = true;
            while (inventorying)
            {
                iface.DrawWindow();
                iface.DrawInventory(items);
                iface.DrawMenu();
                iface.ReadPressedKey();

            }
        }

    }
}
