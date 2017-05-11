using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
	public class WearableItem : Item
	{
		public Ability Power { get; set; }        

        public WearableItem(string name)
		{
			Name = name;
		}

        public WearableItem()
        {

        }

        public static WearableItem Clone(WearableItem ToClone)
        {
            return new WearableItem()
            {
                Name = ToClone.Name,
                Description = ToClone.Description,
                Power = new Ability()
                {
                    Defence = ToClone.Power.Defence,
                    Strength = ToClone.Power.Strength,
                    Inteligence = ToClone.Power.Inteligence
                }
            };
        }
    }
}
