using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class ResourceItem : Item
    {
        public int Quantity { get; set; }
        public int Price { get; set; }

        public ResourceItem()
        {
            Name = this.GetType().Name;
        }
    }
}
