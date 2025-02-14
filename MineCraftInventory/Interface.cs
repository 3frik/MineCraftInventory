using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftInventory
{
    internal class Interface
    {
        //vars
        int INVENTORY_ROWS = 3;
        int INVENTORY_COLUMNS = 9;
        int MENU_AREA_LEFT = 3;
        int MENU_AREA_TOP = 20;
        int INVENTORY_AREA_TOP = 3;
        int INVENTORY_AREA_LEFT = 3;
        int SPACE_BETWEEN_SLOTS = 2;
        int SLOT_WIDTH = 9;
        int SLOT_HEIGHT = 5;
        int activeRow = 0;
        int activeColumn = 0;
        int selectedRow = -1;
        int selectedColumn = -1;
        int WINDOW_HEIGHT = 32;
        int WINDOW_WIDTH = 105;
        int CRAFTING_AREA_TOP = 24;
        int CRAFTING_AREA_LEFT = 58;
        int EQUIPMENT_AREA_TOP = 24;
        int EQUIPMENT_AREA_LEFT = 14;


        public Interface()
        {
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void DrawWindow()
        {   //paint grey
            Console.BackgroundColor = ConsoleColor.Gray;
            /*
            for (int i = 0; i < INVENTORY_AREA_WIDTH; i++)
            {
                for (int j = 0; j < INVENTORY_AREA_HEIGHT; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }*/
            Console.Clear();

            ///DRAW INVENTORY AREA (including backgrounds)
            //active slot
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < SLOT_HEIGHT; i++)
            {
                Console.SetCursorPosition(INVENTORY_AREA_LEFT + activeColumn * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS), INVENTORY_AREA_TOP + activeRow * (SLOT_HEIGHT + SPACE_BETWEEN_SLOTS) + i);
                Console.WriteLine(new string(' ', SLOT_WIDTH));
            }

            //selected slot
            if (selectedRow >= 0 && selectedColumn >= 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                for (int i = 0; i < SLOT_HEIGHT; i++)
                {
                    Console.SetCursorPosition(INVENTORY_AREA_LEFT + selectedColumn * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS), INVENTORY_AREA_TOP + selectedRow * (SLOT_HEIGHT + SPACE_BETWEEN_SLOTS) + i);
                    Console.WriteLine(new string(' ', SLOT_WIDTH));
                }
            }

            //paint white light
            Console.BackgroundColor = ConsoleColor.White;
            //border
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', WINDOW_WIDTH));

            for (int i = 0; i < WINDOW_HEIGHT; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(" ");
            }

            //items slots
            for (int i = 0; i < INVENTORY_COLUMNS; i++)
            {
                for (int j = 0; j < INVENTORY_ROWS; j++)
                {
                    DrawSlotBorder(INVENTORY_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS), INVENTORY_AREA_TOP + j * (SLOT_HEIGHT + SPACE_BETWEEN_SLOTS));
                }
            }

            ///DRAW CRAFTING AREA
            for (int i = 0; i < 3; i++)
            {
                DrawSlotBorder(CRAFTING_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS), CRAFTING_AREA_TOP);
            }

            ///DRAW EQUIPMENT AREA
            for (int i = 0; i < 2; i++)
            {
                DrawSlotBorder(EQUIPMENT_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS), EQUIPMENT_AREA_TOP);
            }

            ///DRAW EQUIPMENT AND CRAFTING ICONS
            Item EquipmentIcon = new Item();
            EquipmentIcon.Sprite =
                "000bb000" +
                "0bbwwbb0" +
                "000bb000" +
                "00b00b00";
            Item CraftingIcon = new Item();
            CraftingIcon.Sprite =
                "0bbbbbb0" +
                "0dddddb0" +
                "000rr000" +
                "000rr000";
            DrawItem(3, EQUIPMENT_AREA_TOP + 1, EquipmentIcon);
            DrawItem(CRAFTING_AREA_LEFT - SLOT_WIDTH - SPACE_BETWEEN_SLOTS, CRAFTING_AREA_TOP + 1, CraftingIcon);

            //Reset
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private void DrawSlotBorder(int Xcoor, int Ycoor)
        {
            //Draw White
            Console.BackgroundColor = ConsoleColor.White;
            //items slots
            Console.SetCursorPosition(Xcoor, Ycoor + SLOT_HEIGHT);
            Console.Write(new string(' ', SLOT_WIDTH));
            for (int k = 0; k < SLOT_HEIGHT; k++)
            {
                Console.SetCursorPosition(Xcoor + SLOT_WIDTH, Ycoor + k);
                Console.WriteLine(" ");
            }
            //Draw Dark Grey
            Console.BackgroundColor = ConsoleColor.DarkGray;
            //items slots
            Console.SetCursorPosition(Xcoor, Ycoor);
            Console.Write(new string(' ', SLOT_WIDTH));
            for (int k = 0; k < SLOT_HEIGHT; k++)
            {
                Console.SetCursorPosition(Xcoor, Ycoor + k);
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// Draws the items in the iventory on screen
        /// </summary>
        /// <param name="items">Items to show</param>
        public void DrawInventory(Item[] items)
        {
            int columns = items.Length / INVENTORY_COLUMNS;
            for (int i = 0; i < items.Length; i++)
            {
                int row = i / INVENTORY_COLUMNS;
                int column = i % INVENTORY_COLUMNS;

                if (items[i] != null)
                {
                    DrawItem(
                        column * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS) + 1 + INVENTORY_AREA_LEFT,
                        row * (SLOT_HEIGHT + SPACE_BETWEEN_SLOTS) + 1 + INVENTORY_AREA_TOP,
                        items[i]);
                }
            }
        }

        /// Draws the items in the Craftingslots on screen
        /// </summary>
        /// <param name="items">Items to show</param>
        public void DrawCraftingItems(Item[] items)
        {
            int columns = items.Length;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    DrawItem(
                        CRAFTING_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS) + 1,
                        CRAFTING_AREA_TOP + 1,
                        items[i]);
                }
            }
        }

        /// Draws the items in the Equipment slots on screen
        /// </summary>
        /// <param name="items">Items to show</param>
        public void DrawEquipmentItems(Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    DrawItem(
                        EQUIPMENT_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS) + 1,
                        EQUIPMENT_AREA_TOP + 1,
                        items[i]);
                }
            }
        }

        /// <summary>
        /// Returns the index of the active index. 
        /// If the item is on the Equipment or Crafting slot, it returns a negative index as follows:
        /// -Equipment slots: -1, -2
        /// -Crafting slots: -5,-6,-7
        /// </summary>
        /// <returns></returns>
        public int ActiveItemIndex()
        {
            int activeIndex = activeColumn + activeRow * INVENTORY_COLUMNS;
            if (activeIndex > INVENTORY_COLUMNS * (INVENTORY_ROWS))
            {
                activeIndex -= INVENTORY_COLUMNS * (INVENTORY_ROWS);
                activeIndex *= -1;
            }
            return activeIndex;
        }

        public int ActiveItemIndexInEquipmentInventory()
        {
            return activeColumn-1;

        }

        public int ActiveItemIndexInCraftingInventory()
        {
            return activeColumn - 6;

        }

        /// <summary>
        /// Draws an item in the inventory
        /// </summary>
        /// <param name="Xcoor">position on screens X axis of the sprite</param>
        /// <param name="Ycoor">position on screen Y axis of the sprite</param>
        /// <param name="item">the item to represent</param>
        private void DrawItem(int Xcoor, int Ycoor, Item item)
        {
            for (int i = 0; i < item.Sprite.Length; i++)    //for each pixel
            {
                int row = i / (SLOT_WIDTH - 1);
                int col = i % (SLOT_WIDTH - 1);
                Console.SetCursorPosition(Xcoor + col, Ycoor + row); //put cursor on pixels position
                switch (item.Sprite[i]) //adjust background colour
                {
                    case 'r':
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    case 'w':
                        Console.BackgroundColor = ConsoleColor.White;
                        break;
                    case 'g':
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;
                    case 'y':
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                    case 'b':
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    case 'd':
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        break;
                    case 'm':
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        break;
                    default:
                        Console.BackgroundColor = ConsoleColor.Gray;
                        break;
                }
                if (item.Sprite[i] != '0') //if there is any colour to draw
                {
                    Console.Write(" ");
                }
            }
            if (item.Ammount > 1)   //if there is a noticeable stack of items
            {
                Console.SetCursorPosition(Xcoor, Ycoor);
                Console.Write(item.Ammount);
            }
            Console.BackgroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Reads the key that is pressed
        /// </summary>
        public void MoveInventory(ConsoleKey pressedKey)
        {
            switch (pressedKey)
            {
                case ConsoleKey.DownArrow:
                    activeRow++;
                    if (activeRow > INVENTORY_ROWS)
                    {
                        activeRow = 0;
                    }
                    else if (activeRow == INVENTORY_ROWS)
                    {
                        if (activeColumn == 0 || activeColumn == 4)
                        {
                            activeColumn++;
                        }
                        else if (activeColumn == 3 || activeColumn == 8)
                        {
                            activeColumn--;
                        }
                    }

                    break;
                case ConsoleKey.UpArrow:
                    activeRow--;
                    if (activeRow < 0)
                    {
                        activeRow = INVENTORY_ROWS;
                        if (activeColumn == 0 || activeColumn == 4)
                        {
                            activeColumn++;
                        }
                        else if (activeColumn == 3 || activeColumn == 8)
                        {
                            activeColumn--;
                        }
                    }
                    break;
                case ConsoleKey.RightArrow:
                    activeColumn++;
                    if (activeColumn == INVENTORY_COLUMNS)
                    {
                        activeColumn = 0;
                    }
                    if (activeRow == INVENTORY_ROWS)
                    {
                        if (activeColumn == 0 || activeColumn == 8)
                        {
                            activeColumn = 1;
                        }
                        else if (activeColumn == 3 || activeColumn == 4)
                        {
                            activeColumn = 5;
                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    activeColumn--;
                    if (activeColumn < 0)
                    {
                        activeColumn = INVENTORY_COLUMNS - 1;
                    }
                    if (activeRow == INVENTORY_ROWS)
                    {
                        if (activeColumn == 3 || activeColumn == 4)
                        {
                            activeColumn = 2;
                        }
                        else if (activeColumn == 0 || activeColumn == 8)
                        {
                            activeColumn = 7;
                        }
                    }
                    break;
                case ConsoleKey.Enter:
                    selectedColumn = activeColumn;
                    selectedRow = activeRow;
                    break;
            }
        }
    }
}
