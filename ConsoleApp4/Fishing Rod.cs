using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Fishing_Rod : ToolItem
    {
        public Fishing_Rod()
        {
            Name = "Fishing Rod";            
            Description = "Allows you to catch fish";
            Power = new Ability();
            Price = 4000; Requirements = 40;            
        }
    }
}
