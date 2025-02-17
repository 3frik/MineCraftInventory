using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
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
        public int Ammount = 1;
        public bool isStackable = false;
        public int MaxAmmount = 1;
        public string Name;
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
        public virtual Item Clone(int ammount = 1)
        {
            return new Item();
        }
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

    }

    /// <summary>
    /// First Child Equipment
    /// </summary>
    internal class Equipment : Item
    {
        public override void Use()
        {

        }

    }

    /// <summary>
    /// Second Child Equipment/Weapon
    /// </summary>
    internal class Weapon : Equipment
    {
        public string Name;

        public Weapon(int ammount = 1)
        {
            Ammount = ammount;
            Name = "weapon";
            Sprite =
                "000bb0bb" +
                "0000wbb0" +
                "00ww00b0" +
                "ww000000";
        }

        public override Item Clone(int ammount = 1)
        {
            return new Weapon(this.Ammount);
        }

    }

    /// <summary>
    /// Second Child Equipment/Weapon
    /// </summary>
    internal class Bow : Equipment
    {
        public string Name;

        public Bow (int ammount = 1)
        {
            Ammount = ammount;
            Name = "weapon";
            Sprite =
                "0000mmmm" +
                "00mm0000" +
                "0m000000" +
                "0m000000";
        }

        public override Item Clone(int ammount = 1)
        {
            return new Bow(this.Ammount);
        }

    }
    /// <summary>
    /// Second Child Equipment/Shield
    /// </summary>
    internal class Shield : Equipment
    {
        public Shield(int ammount = 1)
        {
            Ammount = ammount;
            Name = "Shield";
            Sprite =
                "0bbbbbb0" +
                "0brrrrb0" +
                "0brrrrb0" +
                "00bbbb00";
        }

        public override Item Clone(int ammount = 1)
        {
            return new Shield(this.Ammount);
        }
    }

    /// <summary>
    /// Second class Consumable/Potion
    /// </summary>
    internal class Potion : Consumable
    {
        public Potion(int ammount = 1)
        {
            Ammount = ammount;
            Name = "Potion";
            isStackable = true;
            MaxAmmount = 16;
            Sprite =
                "00wwww00" +
                "000ww000" +
                "00wrrw00" +
                "0wrrrrw0";
        }

        public override void Use()
        {
            this.Ammount--;
            Console.Beep(300, 300);
        }
        public override Item Clone(int ammount = 1)
        {
            return new Potion(this.Ammount);
        }
    }

    internal class Apple : Consumable
    {
        public Apple(int ammount = 1)
        {
            Ammount = ammount;
            Name = "Apple";
            isStackable = true;
            MaxAmmount = 64;
            Sprite =
                "00rrgr00" +
                "0rrrrwr0" +
                "0rrrrrr0" +
                "00rrrr00";
        }
        public override Item Clone(int ammount = 1)
        {
            return new Apple(this.Ammount);
        }

    }

    internal class Iron : Material
    {
        public Iron(int ammount = 1)
        {
            Ammount = ammount;
            Name = "Iron";
            isStackable = true;
            MaxAmmount = 64;
            Sprite =
                "0000www0" +
                "000wdddb" +
                "00wdddb0" +
                "0wdddb00";
        }
        public override Item Clone(int ammount = 1)
        {
            return new Iron(this.Ammount);
        }
    }

    internal class Wood : Material
    {
        public Wood(int ammount = 1)
        {
            Ammount = ammount;
            Name = "Wood";
            isStackable = true;
            MaxAmmount = 64;
            Sprite =
                "000mm000" +
                "0000mmmm" +
                "00mmm000" +
                "0mmm0000";
        }
        public override Item Clone(int ammount = 1)
        {
            return new Wood(this.Ammount);
        }
    }
}
