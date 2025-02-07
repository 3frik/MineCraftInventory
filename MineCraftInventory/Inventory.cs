using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftInventory
{
    internal class Inventory
    {
        private Item[] items = new Item[27];
        private Item[] equipments = new Item[2];
        private Item[] craftings = new Item[3];
        private Interface iface = new Interface();

        public Inventory()
        {
            AddItemToInventory(new Weapon());
            AddItemToInventory(new Wood(20));
            AddItemToInventory(new Wood(30));
            AddItemToInventory(new Potion());
            AddItemToInventory(new Wood(30));
            AddItemToInventory(new Shield());
            AddItemToInventory(new Iron(30));
            AddItemToInventory(new Apple(6));
        }

        public void Run()
        {
            bool inventorying = true;
            while (inventorying)
            {
                iface.DrawWindow();
                iface.DrawInventory(items);
                iface.DrawCraftingItems(craftings);
                iface.DrawEquipmentItems(equipments);
                ReadPressedKey();
            }
        }

        /// <summary>
        /// Adds an item to the inventory. MAy stack and add several
        /// </summary>
        /// <param name="item"></param>
        public void AddItemToInventory(Item item)
        {
            //check if the item is in the inventory, but only for stackable items
            if (item.isStackable)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == item && !(items[i].Ammount < items[i].MaxAmmount))
                    {   //if the item is in the inventory and there is place for more items
                        items[i].Ammount += item.Ammount;   //then add the items
                        if (items[i].Ammount < items[i].MaxAmmount) //but if you put too many items, over MAX
                        {
                            item.Ammount = items[i].Ammount - items[i].MaxAmmount;  //then you have some items left to add to the inventory
                            items[i].Ammount = items[i].MaxAmmount; //and the stack becomes full
                        }
                        else //but if you instead dont get to MAX
                        {
                            item.Ammount = 0; //empty the ammount fo items
                            break;
                        }
                    }
                }
            }
            // if the item is not stackable or it has not been able to put all its stack in existing stacks
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)   //the first empty slot
                {
                    items[i] = item;    //becomes the item
                    break;
                }
            }

        }

        /// <summary>
        /// Removes an optional ammount of items from the inventory
        /// </summary>
        /// <param name="inventoryIndex"></param>
        private void RemoveItemFromInventory(int inventoryIndex, int ammountToRemove = 1)
        {
            items[inventoryIndex].Ammount -= ammountToRemove;
            
            if (items[inventoryIndex].Ammount < 1)
            {
                items[inventoryIndex] = null;
            }
        }

        /// <summary>
        /// Adds one item to the Equipments if there is place for it. The item should be Equipement.
        /// </summary>
        /// <param name="item"></param>
        private void AddItemToEquipment(Item item)
        {
            for (int i = 0; i < equipments.Length; i++)
            {
                if (equipments[i] == null)
                {   //if the item is in the inventory and there is place for more items
                    equipments[i] = item;   //then add the items
                    break;
                }
            }
        }

        /// <summary>
        /// Remvoes one item from the equipments and tryes to put it on the inventory
        /// </summary>
        /// <param name="equipmentIndex"></param>
        private void RemoveItemFromEquipment(int equipmentIndex)
        {
            AddItemToInventory(equipments[equipmentIndex]);
            equipments[equipmentIndex] = null;
        }


    /// <summary>
    /// Adds an item to the craftings inventory. The item should be of tipe material
    /// </summary>
    /// <param name="item"></param>
        private void AddItemToCrafting(Item item, int ammountToAdd = 1)
        {
            for (int i = 0; i < craftings.Length; i++)
            {
                if (craftings[i] == null)
                {   //if the item is in the inventory and there is place for more items
                    Console.WriteLine(" vs "+item.Ammount );
                    craftings[i] = item;    
                    craftings[i].Ammount = ammountToAdd;
                    Console.ReadKey();
                    break;
                }
            }

        }

        /// <summary>
        /// Removes an item from the craftings inventory and tryes to put it on inventory
        /// </summary>
        /// <param name="craftingIndex"></param>
        private void RemoveItemFromCrafting(int craftingIndex)
        {
            AddItemToInventory(craftings[craftingIndex]);
            craftings[craftingIndex] = null;
        }

        /// <summary>
        /// Returns the item in the selected position
        /// </summary>
        /// <returns></returns>
        private Item SelectActiveItem()
        {
            int itemIndex = iface.ActiveItemIndex();
            if (itemIndex > -1)
            {
                return items[itemIndex];
            }
            else if (itemIndex > -3)
            {
                itemIndex++;
                itemIndex *= -1;
                return equipments[itemIndex];
            }
            else
            {
                itemIndex += 4;
                itemIndex *= -1;
                return craftings[itemIndex];
            }

        }

        /// <summary>
        /// Read a pressed key and does or orders to do things according to the key.
        /// </summary>
        public void ReadPressedKey()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey();
            }
            while (!Console.KeyAvailable) { }
            ConsoleKey pressedKey = Console.ReadKey(true).Key;
            switch (pressedKey)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.UpArrow:
                case ConsoleKey.RightArrow:
                case ConsoleKey.LeftArrow:
                    iface.MoveInventory(pressedKey);
                    break;
                case ConsoleKey.Enter:
                    iface.MoveInventory(pressedKey);
                    UseItem(SelectActiveItem());
                    break;
            }

        }

        private void UseItem(Item item)
        {
            if(item is Equipment)
            {
                AddItemToEquipment(item);
                RemoveItemFromInventory(iface.ActiveItemIndex());
            }else if(item is Material)
            {
                AddItemToCrafting(item,1);
                RemoveItemFromInventory(iface.ActiveItemIndex(),1);
            }
        }
    }
}
