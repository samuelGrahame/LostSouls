using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static ConsoleApp4.Program;

namespace ConsoleApp4
{
	public class Humanoid
	{
		public string Name { get; set; }
		public SkinColor SkinColor { get; set; }
		public Gender Gender { get; set; }
		public Race Race { get; set; }
		public LegsItem Legs { get; set; }
		public ChestItem Chest { get; set; }
		public WeaponItem Hands { get; set; }
		public FeetItem Feet { get; set; }
		public HeadItem Head { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Age { get; set; }
        public int AvailablePowerPoints { get; set; } = 0;
        public Level CombatLevel { get; set; }
        public ToolItem Belt { get; set; }
        public List<Quest> ActivatedQuests = new List<Quest>();
                
        public Level Smithing { get; set; } = new Level();
        public Level Barter { get; set; } = new Level();

        public void SetRandomNewName()
        {
            if(Gender == Gender.Female)
            {
                Name = FemaleNames.GetRandomName();
            }
            else
            {
                Name = MaleNames.GetRandomName();
            }
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
        
        public Humanoid()
        {            
            CombatLevel = new Level((newLevel) => {
                MaxHealth = newLevel * 10;
                AddHealth(10);
                if (Program.ActiveWorld.MainCharacter == this)
                {
                    AvailablePowerPoints++;
                    Program.UpdateStats(this);
                }
                else
                {
                    var x = GetWearablePower();

                    switch (Race)
                    {
                        case Race.Orc:
                            if(Program.ActiveWorld.Percent(50))
                            {
                                if (Program.ActiveWorld.Percent(75))
                                {
                                    Power.Strength += 1;
                                }
                                else if (Program.ActiveWorld.Percent(75))
                                {
                                    Power.Defence += 1;
                                }
                                else
                                {
                                    Power.Inteligence += 1;
                                }
                                return;
                            }                            
                            break;
                        case Race.Human:
                            if (Program.ActiveWorld.Percent(50))
                            {
                                if(Program.ActiveWorld.Percent(50))
                                {
                                    Power.Strength += 1;
                                }
                                else if (Program.ActiveWorld.Percent(50))
                                {
                                    Power.Defence += 1;
                                }
                                else
                                {
                                    Power.Inteligence += 1;
                                }                                
                                return;
                            }
                            break;
                        case Race.Elf:
                            if (Program.ActiveWorld.Percent(50))
                            {
                                if (Program.ActiveWorld.Percent(40))
                                {
                                    Power.Strength += 1;
                                }
                                else if (Program.ActiveWorld.Percent(25))
                                {
                                    Power.Defence += 1;
                                }
                                else
                                {
                                    Power.Inteligence += 1;
                                }
                                return;
                            }

                            break;
                        default:
                            break;
                    }

                    if(x.Strength == x.Inteligence && x.Inteligence == x.Defence)
                    {
                        switch (Program.ActiveWorld.RandomNumber(3))
                        {
                            case 0:
                                Power.Strength += 1;
                                break;
                            case 1:
                                Power.Defence += 1;
                                break;
                            case 2:
                                Power.Inteligence += 1;
                                break;
                        }
                    }
                    else
                    {
                        if(x.Strength >= x.Inteligence && x.Strength >= x.Defence)
                        {
                            Power.Strength += 1;
                        }
                        else if (x.Inteligence >= x.Strength && x.Inteligence >= x.Defence)
                        {
                            Power.Inteligence += 1;
                        }
                        else
                        {
                            Power.Defence += 1;
                        }
                    }                    
                }
            });
        }

        public void SetLevel(int level)
        {
            if (level <= CombatLevel.Value)
                return;
            
            CombatLevel.AddXP(CombatLevel.GetXPToLevelUp(level) - CombatLevel.XP);
        }

        // Level, XP, XPToLevelUp

        public string GetDescription()
        {
            if(Age < 2)
            {
                return Gender == Gender.Male ? "baby boy" : "baby girl";
            }else if (Age < 16)
            {
                return Gender == Gender.Male ? "boy" : "girl";
            }
            else
            {
                return Gender.ToString("G");
            }
        }

        public void AttackOther(Humanoid other, int damage)
        {
            if (damage < 0)
                damage = 0;

            damage = other.CompressDamage(damage);

            var xp = damage * 4;
            CombatLevel.AddXP(xp);
            
            if (this == Program.ActiveWorld.MainCharacter)
            {
                WriteLineColor($"You did {damage} damage to {(string.IsNullOrWhiteSpace(other.Name) ? "the enemy" : other.Name)}", ConsoleColor.Red, ConsoleColor.Black);
                if(xp > 0)
                {
                    WriteLineColor($"You received {xp} xp", ConsoleColor.DarkCyan, ConsoleColor.Black);
                }               
            }
            else if (other == Program.ActiveWorld.MainCharacter)
            {
                WriteLineColor($"You received {damage} damage from {(string.IsNullOrWhiteSpace(this.Name) ? "the enemy" : this.Name)}", ConsoleColor.Red, ConsoleColor.Black);                
            }
            else
            {
                WriteLineColor($"{(string.IsNullOrWhiteSpace(this.Name) ? "the enemy 1" : this.Name)} did {damage} damage to {(string.IsNullOrWhiteSpace(other.Name) ? "the enemy 2" : other.Name)}", ConsoleColor.Red, ConsoleColor.Black);
            }

            other.AddHealth(-damage);

            if (other == Program.ActiveWorld.MainCharacter)
            {
                WriteLineColor($"{(string.IsNullOrWhiteSpace(this.Name) ? "the enemy" : this.Name)}'s health is now {this.Health}", ConsoleColor.Red, ConsoleColor.Black);
            }

            Program.UpdateStats(Program.ActiveWorld.MainCharacter);
        }

        public void AddHealth(int value)
        {
            if (value == 0)
                return;

            if(value > 0)
            {
                Health += value;
                if (Health > MaxHealth)
                    Health = MaxHealth;
            }
            else
            {
                Health += value;
                if (Health < 0)
                {
                    Health = 0;
                }
            }
        }

        public int CompressDamage(int value)
        {
            var power = GetWearablePower();
            float damageMulitplier;

            if (power.Defence >= 0)
            {
                damageMulitplier = 100.0f / (100.0f + (float)power.Defence);
            }
            else
            {
                damageMulitplier = 2.0f - 100.0f / (100.0f - (float)power.Defence);
            }
            
            value = (int)(value * damageMulitplier);
            return value;
        }

        public int GetAttackDamage()
        {
            return GetWearablePower().Strength;
        }

        public int GetMagicDamage()
        {
            return GetWearablePower().Inteligence;
        }

        public Bag Inventory { get; set; } = new Bag();

		public Ability Power { get; set; } = new Ability();

		public Ability GetWearablePower()
		{
			return Power.CombineAbilities(Legs?.Power, Chest?.Power, Hands?.Power, Feet?.Power, Head?.Power);
		}

		/// <summary>
		/// Orens are of high value to buy and sell items,important.
		/// </summary>
		public int Orens { get; set; }



		// skin color,
		// bag - to hold possesions, 
		// weapon - wearable item - used to attack/harm other things,
		// tool - to mine or gather resouces - stone/wood/food/animal hides,
		// currency - money/something of good value,
		// abillity - strenght/intelligence/aggility,
		// race - elf/orc/human,
		// name - title,
		// partner - children/home,
		// work - merchant/farmer/blacksmith,
		// sex - male/female,
	}

	//Christianity(2.1 billion)
	//Islam(1.3 billion)
	//Nonreligious(Secular/Agnostic/Atheist) (1.1 billion)
	//Hinduism(900 million)
	//Chinese traditional religion(394 million)
	//Buddhism 376 million.
	//Primal-indigenous(300 million)

	public enum SkinColor
	{
		Dark,
		White,
		Pale,
		Red,
		Yellow,
		Creamy,
		Brown
	}

	public enum Race
	{
		Orc,
		Human,
		Elf
	}
}   public enum Gender
    {
		Male,
		Female
    }











