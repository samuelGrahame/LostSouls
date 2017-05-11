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
	}
}
