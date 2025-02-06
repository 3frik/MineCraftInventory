using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftInventory
{
    /// <summary>
    /// Base Class Item
    /// </summary>
    internal class Item
    {
        public string Sprite { get; set; }
        public int Ammount;
        public bool isStackable = false;
        public int MaxAmmount = 1;
        public Item()
        {
            Ammount = 1;
           
            Sprite =
                "00000b00" +
                "00000yy0" +
                "0yyyyyy0" +
                "00yyyy00";

        }

        public virtual void Use() { }
    }

    /// <summary>
    /// First Child Consumable
    /// </summary>
    internal class Consumable : Item
    {
        public override void Use()
        {
            Console.WriteLine("You use this item.");
            Ammount--;
            if (Ammount < 1)
            {
                // RemoveItem();
            }
        }
    }

    /// <summary>
    /// First Child Material
    /// </summary>
    internal class Material : Item
    {
        public override void Use()
        {
            Console.WriteLine("Ýou use this material.");
            Ammount--;
        }
    }

    /// <summary>
    /// First Child Equipment
    /// </summary>
    internal class Equipment : Item
    {

    }

    /// <summary>
    /// Second Child Equipment/Weapon
    /// </summary>
    internal class Weapon : Equipment
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

    /// <summary>
    /// Second Child Equipment/Shield
    /// </summary>
    internal class Shield : Equipment
    {
        public Shield()
        {
            Ammount = 1;
            Sprite =
                "0bbbbbb0" +
                "0brrrrb0" +
                "0brrrrb0" +
                "00bbbb00";
        }
    }

    /// <summary>
    /// Second class Consumable/Potion
    /// </summary>
    internal class Potion : Consumable
    {
        public Potion()
        {
            Ammount = 1;
            isStackable = true;
            MaxAmmount = 16;
            Sprite =
                "00wwww00" +
                "000ww000" +
                "00wrrw00" +
                "0wrrrrw0";
        }
    }

    internal class Apple : Consumable
    {
        public Apple()
        {
            Ammount = 1;
            isStackable = true;
            MaxAmmount = 16;
            Sprite =
                "00rrgr00" +
                "0rrgrrr0" +
                "0rrrrwr0" +
                "00rrrr00";
        }

    }
}
