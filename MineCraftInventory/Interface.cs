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
        int WINDOW_HEIGHT = 25;
        int WINDOW_WIDTH = 105;


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
                    Console.SetCursorPosition(INVENTORY_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS), INVENTORY_AREA_TOP + j * (SLOT_HEIGHT + SPACE_BETWEEN_SLOTS) + SLOT_HEIGHT);
                    Console.Write(new string(' ', SLOT_WIDTH));
                    for (int k = 0; k < 5; k++)
                    {
                        Console.SetCursorPosition(INVENTORY_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS) + SLOT_WIDTH, INVENTORY_AREA_TOP + j * (SLOT_HEIGHT + SPACE_BETWEEN_SLOTS) + k);
                        Console.WriteLine(" ");
                    }
                }
            }

            //paint dark shadow
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(0, WINDOW_HEIGHT);
            Console.Write(new string(' ', WINDOW_WIDTH));

            for (int i = 0; i < WINDOW_HEIGHT + 1; i++)
            {
                Console.SetCursorPosition(WINDOW_WIDTH, i);
                Console.Write(" ");
            }
            //items slots
            for (int i = 0; i < INVENTORY_COLUMNS; i++)
            {
                for (int j = 0; j < INVENTORY_ROWS; j++)
                {
                    Console.SetCursorPosition(INVENTORY_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS), INVENTORY_AREA_TOP + j * (SLOT_HEIGHT + SPACE_BETWEEN_SLOTS));
                    Console.Write(new string(' ', SLOT_WIDTH));
                    for (int k = 0; k < 5; k++)
                    {
                        Console.SetCursorPosition(INVENTORY_AREA_LEFT + i * (SLOT_WIDTH + SPACE_BETWEEN_SLOTS), INVENTORY_AREA_TOP + j * (SLOT_HEIGHT + SPACE_BETWEEN_SLOTS) + k);
                        Console.WriteLine(" ");
                    }
                }
            }


            //Reset
            Console.BackgroundColor = ConsoleColor.Black;
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

        /// <summary>
        /// Draws the options in the menu adn asks to chose one
        /// </summary>
        public void DrawMenu()
        {

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

        public void ReadPressedKey()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey();
            }
            while (!Console.KeyAvailable) { }

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.DownArrow:
                    activeRow++;
                    if (activeRow == INVENTORY_ROWS)
                    {
                        activeRow = 0;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    activeRow--;
                    if (activeRow < 0)
                    {
                        activeRow = INVENTORY_ROWS - 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    activeColumn++;
                    if (activeColumn == INVENTORY_COLUMNS)
                    {
                        activeColumn = 0;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    activeColumn--;
                    if (activeColumn < 0)
                    {
                        activeColumn = INVENTORY_COLUMNS - 1;
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
