using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class WoodCutters_Axe : ToolItem
    {

        public WoodCutters_Axe()
        {
            Name = "WoodCutter's Axe";
            Description = "Allows you to cut tree's down";
            Power = new Ability();
            Price = 1500; Requirements = 15;
        }
    }
}
