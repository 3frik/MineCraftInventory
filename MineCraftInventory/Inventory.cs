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
            items[0].Ammount = 1;
            items[1] = new Item();
            items[1].Ammount = 37;
            items[2] = new Weapon();
            items[3] = new Potion();
            items[4] = new Shield();
            items[15] = new Apple();
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

        public void AddItem(Item item)
        {
            //check if the item is in the inventory
            if (item.isStackable)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == item) {
                        //items[i] += item.Ammount;
                    }
                } 
            }
        }
    }
}
