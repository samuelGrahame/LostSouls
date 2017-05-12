using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Hammer : ToolItem
    {
        public Hammer()
        {
            Name = "Hammer";
            Description = "Allows you to build bridge's";
            Power = new Ability();
            Price = 2000; Requirements = 20;
        }
    }
}
