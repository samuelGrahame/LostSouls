using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
	public class WeaponItem : WearableItem
	{		
		public WeaponItem(string name) : base(name)
		{
		}
        public WeaponItem()
        {

        }

        public static new WeaponItem Clone(WearableItem ToClone)
        {
            return new WeaponItem()
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
