using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class QuestItem : ToolItem
    {
        public QuestItem()
        {
            Name = "Quest Crystal";
            Description = "When you activate the crystal, You will receive a quest.";
            Price = 100;
            Power = new Ability();
        }
    }
}
//(Quest 6("Stranger: I Need some wood to build? 

//    newBorn.Power.Defence += 1;
//    newBorn.Power.Inteligence += 1;
//    newBorn.Power.Strength += 1;

//    UpdateStats(newBorn);