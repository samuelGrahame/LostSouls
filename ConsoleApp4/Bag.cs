using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
	public class Bag : Item
	{
		public List<Item> Items { get; set; } = new List<Item>();

		public Bag()
		{

		}

        public bool CanIUseResource(Type type, int Quantity)
        {
            var x = Activator.CreateInstance(type) as ResourceItem;
            if (!(x is ResourceItem))
                return false;

            foreach (var item1 in Items)
            {
                if (item1 is ResourceItem && item1.GetType() == type)
                {                     
                    if (Quantity <= (item1 as ResourceItem).Quantity)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }                    
                }
            }

            return false;
        }

        public void AddOrRemoveResourceItem(Type type, int Quantity)
        {
            var x = Activator.CreateInstance(type) as ResourceItem;
            if (!(x is ResourceItem))
                return;

            foreach (var item1 in Items)
            {
                if(item1 is ResourceItem && item1.GetType() == type)
                {
                    (item1 as ResourceItem).Quantity += Quantity;
                    if((item1 as ResourceItem).Quantity <= 0)
                    {
                        Items.Remove(item1);
                    }
                    return;
                }
            }
            if(Quantity > 0)
            {                
                x.Quantity = Quantity;

                Items.Add(x);
            }            
        }        
    }
}
