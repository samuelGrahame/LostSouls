using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Quest
    {
        public QuestType Type { get; set; }
        public int QuantityTillFinished { get; set; }
        public int StartDay { get; set; }
        public int InternalReference { get; set; }
    }

    public enum QuestType
    {
        GatherTrees,
        GatherFish,
        GatherStrangers,
        FightOrc,
        FightElfs,
        FightHumans
    }
}
