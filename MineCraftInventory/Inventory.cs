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
            AddItemToInventory(new Potion(20));
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
                    if (items[i] != null)   //Check for every item that is there in the inventory
                    {

                        bool isSameItem = items[i].Name == item.Name;
                        bool isFull = items[i].Ammount >= items[i].MaxAmmount;
                        if (isSameItem && !isFull)  //if the item is of the same type and there is room for more items
                        {
                            items[i].Ammount += item.Ammount;   //then add ALL the items
                            if (items[i].Ammount > items[i].MaxAmmount) //but if you put too many items, over MAX
                            {
                                item.Ammount = items[i].Ammount - items[i].MaxAmmount;  //then you have some items left to add to the inventory in some other way
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
            }   //if the item is not stackable OR it is, but all stacks have been fully stacked
            for (int i = 0; i < items.Length; i++)
            {
                if (item.Ammount > 0 && items[i] == null)   //as long there are items to save and an empty slot is found
                {
                    items[i] = item.Clone();   //create in the slot a new item      
                    items[i].Ammount = item.Ammount;    //dump all items in the slot
                    item.Ammount -= items[i].MaxAmmount;    //Remove a whole stack from the original item (may become negative)
                    if (items[i].Ammount > items[i].MaxAmmount) //if we had dumped more than a stack
                    {
                        items[i].Ammount = items[i].MaxAmmount; //adjust the ammount to the stack
                    }
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
        private bool AddItemToEquipment(Item item)
        {
            for (int i = 0; i < equipments.Length; i++)
            {
                if (equipments[i] == null)
                {   //if the item is in the inventory and there is place for more items
                    equipments[i] = item.Clone();   //then add the items
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Remvoes one item from the equipments and tryes to put it on the inventory
        /// </summary>
        /// <param name="equipmentIndex"></param>
        private void RemoveItemFromEquipment(int equipmentIndex)
        {
            equipments[equipmentIndex] = null;
        }


        /// <summary>
        /// Adds an item to the craftings inventory. The item should be of tipe material
        /// </summary>
        /// <param name="item"></param>
        private void AddItemToCrafting(Item item, int ammountToAdd = 1)
        {
            for (int i = 0; i < craftings.Length-1; i++)
            {
                if (craftings[i] == null)
                {   //if the item is in the inventory and there is place for more items
                    craftings[i] = item.Clone();                                        
                    craftings[i].Ammount = ammountToAdd;
                    break;
                }
            }
            UpdateCraftingResult();
        }

        private void UpdateCraftingResult()
        {
            if (craftings[0] != null && craftings[1] != null)
            {
                List<Type> types = new();
                types.Add(craftings[0].GetType());
                types.Add(craftings[1].GetType());
                if (types.Contains(new Iron().GetType()))
                {
                    if (types.Contains(new Wood().GetType()))
                    {
                        craftings[2] = new Shield();
                    }
                    else
                    {
                        craftings[2] = new Weapon();
                    }
                }
            }
            else
            {
                craftings[2] = null;
            }
        }

        /// <summary>
        /// Removes an item from the craftings inventory and tryes to put it on inventory
        /// </summary>
        /// <param name="craftingIndex"></param>
        private void RemoveItemFromCrafting(int craftingIndex)
        {
            AddItemToInventory(craftings[craftingIndex].Clone());
            craftings[craftingIndex] = null;
            UpdateCraftingResult();
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
            else if (itemIndex > -3) // this item is in the equipment inventory
            {
                itemIndex++;
                itemIndex *= -1;
                return equipments[itemIndex];
            }
            else    //This item is in the crafting inventory
            {
                itemIndex += 5;
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
                    switch (iface.ActiveItemIndex())
                    {
                        case > -1:
                            UseItem(SelectActiveItem());
                            break;
                        case > -3:
                            int index = iface.ActiveItemIndexInEquipmentInventory();
                            AddItemToInventory(equipments[index]);
                            RemoveItemFromEquipment(iface.ActiveItemIndexInEquipmentInventory());
                            break;
                        case > -8:
                            AddItemToInventory(craftings[iface.ActiveItemIndexInCraftingInventory()]);
                            if (iface.ActiveItemIndexInCraftingInventory() == 2)
                            {
                                craftings = new Item[3];
                            }
                            else
                            {
                                RemoveItemFromCrafting(iface.ActiveItemIndexInCraftingInventory());
                            }
                                break;
                    }
                    break;
            }

        }

        private void UseItem(Item item)
        {
            if (item is Equipment)
            {
                if (AddItemToEquipment(item))
                {
                    RemoveItemFromInventory(iface.ActiveItemIndex());
                }
            }
            else if (item is Material)
            {
                AddItemToCrafting(item, 1);
                RemoveItemFromInventory(iface.ActiveItemIndex(), 1);
            }
        }
    }
}
