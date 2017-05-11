using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
	public class Item
	{
		public string Name { get; set; }
        public string Description { get; set; }

        public Item()
		{

		}

		public Item(string name)
		{
			Name = name;
		}
	}
}
