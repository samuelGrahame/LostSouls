using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
	public class Ability
	{
		public int Strength { get; set; }		
		public int Inteligence { get; set; }
		public int Defence { get; set; }

        public float GetTotalPower()
        {
            return Strength + Inteligence + Defence;
        }

		public Ability CombineAbilities(params Ability[] args)
		{
			var x = new Ability() { Strength = Strength, Defence = Defence, Inteligence = Inteligence };

			foreach (var item in args)
			{
                if (item == null)
                    continue;
				x.Strength += item.Strength;
				x.Inteligence += item.Inteligence;
				x.Defence += item.Defence;
			}

			return x;
		}
	}
}
