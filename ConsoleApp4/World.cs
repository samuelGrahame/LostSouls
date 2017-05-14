using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ConsoleApp4.Program;
using static System.Console;
using System.Windows;
using System.Collections;
using System.Drawing;
using System.Threading;
using Newtonsoft.Json;

namespace ConsoleApp4
{
	public class World
	{
        public Humanoid MainCharacter { get; set; }
        private bool running;
        public List<Humanoid> Humanoids { get; set; } = new List<Humanoid>();
        public Location CurrentLocation = Location.LocationSpot;
        public Buildings ActiveBuilding = null;
        public LocationSpot HomeTown = null;
        public List<LocationSpot> Locations = new List<LocationSpot>();
        public int Day = 0;
        public int DayLastSlept = 0;
        public int TimeInDay = 7;

        public bool IsDayTime()
        {
            return TimeInDay >= 7 && TimeInDay < 19;
        }

        public int TotalStrangersCollected = 0;
        public int TotalElfsDefeated = 0;
        public int TotalHumansDefeated = 0;
        public int TotalOrcsDefeated = 0;

        public void IncrementTime(int number, bool updateScreen = true)
        {
            if (number == 0)
                return;

            TimeInDay += number;
            while(TimeInDay > 24)
            {
                TimeInDay -= 24;
                Day += 1;
            }
            if(updateScreen)
            {
                UpdateStats(MainCharacter);
                stats.UpdateWorld(this);
            }            
        }
                
        [JsonIgnoreAttribute]
        public Bitmap MapRawData;
        public string MapRawDataString;

        public MapItem[,] Map = null;

        public LocationSpot ActiveLocation = null;
        public ForestEvent CurrentForestEvent = ForestEvent.None;

        public int CurrentX { get; set; } = 0;
        public int CurrentY { get; set; } = 0;

        public Random random = new Random();
        
        public int RandomNumber(int value)
        {            
            return random.Next(value);
        }

        public bool Percent(decimal value)
        {
            return random.Next((int)(100 / value)) == 0;
        }

        public Humanoid Born(Humanoid humanoid)
		{
			Humanoids.Add(humanoid);
			return humanoid;
		}

		public void Death(Humanoid humanoid)
		{
			Humanoids.Remove(humanoid);
		}

        public Quest GetRandomQuestion()
        {
            var quest = new Quest() { QuantityTillFinished = random.Next(1, 100), Type = (QuestType)random.Next(6), StartDay = Day };

            switch (quest.Type)
            {             
                case QuestType.GatherStrangers:
                    quest.InternalReference = TotalStrangersCollected;
                    break;
                case QuestType.FightOrc:
                    quest.InternalReference = TotalOrcsDefeated;
                    break;
                case QuestType.FightElfs:
                    quest.InternalReference = TotalElfsDefeated;
                    break;
                case QuestType.FightHumans:
                    quest.InternalReference = TotalHumansDefeated;
                    break;
                default:
                    break;
            }

            return quest;
        }

        public void ProcessBuyOrMakeItem(IList Items, string Caption)
        {
            bool inList = true;
            
            string input;

            do
            {
                List<Tuple<string, string, Action>> Commands1 = new List<Tuple<string, string, Action>>();

                foreach (var item in Items)
                {
                    WearableItem wearableItem = null;
                    if(item is WearableItem)
                    {
                        wearableItem = (WearableItem)item;
                    }
                    if(wearableItem != null)
                    {
                        var price = item is ToolItem ? (item as ToolItem).Price : (wearableItem.Power.Strength + 1) * (wearableItem.Power.Inteligence + 1) * (wearableItem.Power.Defence + 1) *
                            (ActiveBuilding.Type == BuildingType.Blacksmith ? ActiveBuilding.Owner.Smithing.Value : 1);                            
                        
                        Commands1.Add(new Tuple<string, string, Action>($"{Commands1.Count}: {wearableItem.Name}, price: { GetOrenLabel(price) }", $"{wearableItem.Name},{Commands1.Count}", () =>
                        {
                            if (Question($"{ActiveBuilding.Owner.Name}: Are you sure you would like to buy a {wearableItem.Name} for { GetOrenLabel(price) }?", "yes", "no") == "yes")
                            {
                                if (MainCharacter.Orens >= price)
                                {
                                    MainCharacter.Orens -= price;

                                    if(ActiveBuilding.Type == BuildingType.Blacksmith)
                                    {
                                        // we are making the item

                                        WearableItem witem = Activator.CreateInstance(wearableItem.GetType()) as WearableItem;
                                        var multiplier = (1.0f + (ActiveBuilding.Owner.Smithing.Value / 100.0f));
                                        witem.Description = wearableItem.Description;
                                        witem.Name = wearableItem.Name;
                                        witem.Power = new Ability() { Strength = (int)(wearableItem.Power.Strength * multiplier), Defence = (int)(wearableItem.Power.Defence * multiplier), Inteligence = (int)(wearableItem.Power.Inteligence * multiplier) };

                                        MainCharacter.Inventory.Items.Add(witem);
                                    }
                                    else
                                    {
                                        MainCharacter.Inventory.Items.Add(wearableItem);
                                    }
                                    
                                    WriteLineColor($"You have know spent {GetOrenLabel(price)}.", ConsoleColor.Yellow, ConsoleColor.Black);
                                    WriteLineColor($"You now have {GetOrenLabel(MainCharacter.Orens)}", ConsoleColor.Yellow, ConsoleColor.Black);

                                    UpdateStats(MainCharacter);
                                }
                                else
                                {
                                    WriteLineColor($"You do not have enough oren's, Please come back when you have enough.", ConsoleColor.Yellow, ConsoleColor.Black);
                                }
                                if (Question($"{ActiveBuilding.Owner.Name}: Was that all you needed {MainCharacter.Name}?", "yes", "no") == "yes")
                                {
                                    inList = false;
                                }
                            }
                        }));
                    }                    
                }

                Commands1.Add(new Tuple<string, string, Action>($"{Commands1.Count}: Nothing, Sorry", $"{Commands1.Count}nothing,sorry", () =>
                {
                    inList = false;
                }));

                List<string> Options1 = new List<string>();
                var Builder1 = new StringBuilder(ActiveBuilding.Owner.Name + $": Which {Caption} would you like me to buy?\r\n");

                foreach (var item in Commands1)
                {
                    Options1.Add(item.Item2);
                    Builder1.AppendLine(item.Item1);
                }

                input = Question(Builder1.ToString(), Options1.ToArray());


                for (int i = 0; i < Options1.Count; i++)
                {
                    if (input == Commands1[i].Item2.ToLower())
                    {
                        Commands1[i].Item3();
                        break;
                    }
                }

            } while (inList);            
        }

        public void Play()
        {
            var thr = new Thread(() => {
                stats.ActiveWorld = this;
                stats.ActiveHuman = MainCharacter;
                stats.ShowDialog();
            });
            thr.SetApartmentState(ApartmentState.STA);
            thr.Start();

            running = true;
 
            Save(false);
            
            string input = "";

            while(running)
            {
                MakeVisible();

                Program.UpdateStats(MainCharacter);                
                Program.stats.UpdateWorld(this);
                
                switch (CurrentLocation)
                {
                    case Location.LocationSpot:
                        ActiveBuilding = null;
                        CurrentForestEvent = ForestEvent.None;
                        if (MainCharacter.Health < MainCharacter.MaxHealth)
                        {
                            MainCharacter.AddHealth(MainCharacter.MaxHealth - MainCharacter.Health);
                            WriteLineColor("Your feel rested and now have full health.", ConsoleColor.Green, ConsoleColor.Black);
                            Program.UpdateStats(MainCharacter);
                            Save();                            
                        }
                        List<Tuple<string, string, Action>> Commands = new List<Tuple<string, string, Action>>();

                        Commands.Add(new Tuple<string, string, Action>($"{Commands.Count}: Explore outside.", $"leave,outside,explore,{Commands.Count}", () => { CurrentLocation = Location.Forest;  }));                        

                        if (ActiveLocation == HomeTown)
                        {                            
                            Commands.Add(new Tuple<string, string, Action>($"{Commands.Count}: Enter your (home) and (sleep).", $"home,sleep,{Commands.Count}", ()=> {
                                if(!IsDayTime())
                                {
                                    IncrementTime(12);
                                    Save();
                                    WriteLineColor("You feel rested...", ConsoleColor.Green, ConsoleColor.Black);
                                }
                                else
                                {
                                    WriteLineColor("You don't feel tired it is only " + TimeInDay + ":00. (24 hr time)", ConsoleColor.Red, ConsoleColor.Black);
                                }                    
                            }));
                        }

                        foreach (var item in ActiveLocation.Buildings)
                        {
                            switch (item.Type)
                            {                                
                                case BuildingType.Store:
                                    Commands.Add(new Tuple<string, string, Action>($"{Commands.Count}: Enter {item.Owner.Name}'s Store.", $"{item.Owner.Name},store,{Commands.Count}", ()=> {
                                        CurrentLocation = Location.Building;
                                        ActiveBuilding = item;
                                    }));
                                    break;
                                case BuildingType.Blacksmith:
                                    Commands.Add(new Tuple<string, string, Action>($"{Commands.Count}: Enter {item.Owner.Name}'s Blacksmith.", $"{item.Owner.Name},blacksmith,{Commands.Count}", () => {
                                        CurrentLocation = Location.Building;
                                        ActiveBuilding = item;
                                    }));
                                    break;
                                default:
                                    break;
                            }
                        }

                        Commands.Add(new Tuple<string, string, Action>($"{Commands.Count}: Close.", $"close,{Commands.Count}", () => {
                            Save();
                            running = false;

                            Application.Exit();
                        }));

                        List<string> Options = new List<string>();
                        var Builder = new StringBuilder($"What would you like to do in {ActiveLocation.Name}?\r\n");

                        foreach (var item in Commands)
                        {
                            Options.Add(item.Item2);
                            Builder.AppendLine(item.Item1);
                        }

                        input = Question(Builder.ToString(), Options.ToArray());


                        for (int i = 0; i < Options.Count; i++)
                        {
                            if(input == Commands[i].Item2.ToLower())
                            {
                                Commands[i].Item3();
                                break;
                            }
                        }

                        break;
                    case Location.Building:
                        CurrentForestEvent = ForestEvent.None;
                        string Type = ActiveBuilding.Type == BuildingType.Blacksmith ? "Can you make me a " : "Can I buy some ";
                        List<string> Commands4 = new List<string>()
                        {
                            "0,weapon","1,head", "2,chest", "3,legs", "4,feet", "5,nothing,sorry,leave", "6,sell"
                        };
                        if(ActiveBuilding.Type == BuildingType.Store)
                        {
                            Commands4.Add("7,tool");
                        }
                        {
                            Commands4.Add("8,Quests");
                        }
                        input = Question(string.Format(
ActiveBuilding.Owner.Name + @": Welcome to my {1:G}, Can I help you with something?
0: {0}weapon's.
1: {0}head wear.
2: {0}chest wear.
3: {0}leg wear.
4: {0}feet wear.
5: Nothing, Sorry.
6: Sell something." + (ActiveBuilding.Type == BuildingType.Store ? "\r\n7: Buy a Tool." : ""), Type, ActiveBuilding.Type), Commands4.ToArray());
                        
                        switch (input)
                        {
                            case "0,weapon":
                                ProcessBuyOrMakeItem(Database.Weapons, "Weapons");

                                break;
                            case "1,head":
                                ProcessBuyOrMakeItem(Database.HeadWear, "Head Wear");

                                break;
                            case "2,chest":
                                ProcessBuyOrMakeItem(Database.ChestWear, "Chest Wear");

                                break;
                            case "3,legs":
                                ProcessBuyOrMakeItem(Database.LegWear, "leg Wear");

                                break;
                            case "4,feet":
                                ProcessBuyOrMakeItem(Database.FeetWear, "feet Wear");

                                break;
                            case "5,nothing,sorry,leave":
                                WriteLine($"You shut {ActiveBuilding.Owner.Name}'s door and leave the {ActiveBuilding.Type.ToString("G").ToLower()}.");
                                CurrentLocation = Location.LocationSpot;
                                ActiveBuilding = null;

                                break;
                            case "6,sell":
                                stats.ToSellSomething = true;
                                Question($"What do you want to sell? Double click on the item you wish to sell", "yes", "no");
                                
                                CurrentLocation = Location.LocationSpot;
                                ActiveBuilding = null;
                                break;
                            case "7,tool":
                                ProcessBuyOrMakeItem(Database.Tools, "Tools");
                      
                                break;
                        }
                        
                        break;
                    case Location.Forest:
                        // some kind of person has appeared.     
                        CurrentForestEvent = ForestEvent.None;
                        List<Tuple<string, string, Action>> Commands3 = new List<Tuple<string, string, Action>>();

                        // get town From X Y
                        var mapItem = Map[CurrentX, CurrentY];
                        mapItem.Visible = true;

                        Commands3.Add(new Tuple<string, string, Action>($"{Commands3.Count}: Go West?", $"west,left,{Commands3.Count}", () => {
                            stats.ProcessMovement(CurrentX - 1, CurrentY);
                        }));
                        Commands3.Add(new Tuple<string, string, Action>($"{Commands3.Count}: Go North?", $"north,up,{Commands3.Count}", () => {
                            stats.ProcessMovement(CurrentX, CurrentY - 1);
                        }));
                        Commands3.Add(new Tuple<string, string, Action>($"{Commands3.Count}: Go East?", $"east,up,{Commands3.Count}", () => {
                            stats.ProcessMovement(CurrentX + 1, CurrentY);
                        }));
                        Commands3.Add(new Tuple<string, string, Action>($"{Commands3.Count}: Go South?", $"south,up,{Commands3.Count}", () => {
                            stats.ProcessMovement(CurrentX, CurrentY + 1);
                        }));

                        if (mapItem is LocationSpot )
                        {
                            ActiveLocation = mapItem as LocationSpot;
                            WriteLineColor($"You have found a town called {ActiveLocation.Name}", ConsoleColor.Yellow, ConsoleColor.Black);

                            Commands3.Add(new Tuple<string, string, Action>($"{Commands3.Count}: Would you like to enter {ActiveLocation.Name}?", $"{ActiveLocation.Name},yes,{Commands3.Count}", () => {
                                CurrentLocation = Location.LocationSpot;
                            }));
                        }
                        else
                        {
                            ActiveLocation = null;
                        }

                        List<string> Options3 = new List<string>();
                        var Builder3 = new StringBuilder($"which direction do you want to go?\r\n");

                        foreach (var item in Commands3)
                        {
                            Options3.Add(item.Item2);
                            Builder3.AppendLine(item.Item1);
                        }

                        if(mapItem is Ground)
                        {
                            ProcessRandomEvent();
                            if (CurrentLocation == Location.LocationSpot)
                                continue;
                        }

                        input = Question(Builder3.ToString(), Options3.ToArray());

                        for (int i = 0; i < Options3.Count; i++)
                        {
                            if (input == Commands3[i].Item2.ToLower())
                            {
                                Commands3[i].Item3();
                                break;
                            }
                        }                        

                        if(CurrentLocation == Location.Forest)
                        {
                            WriteLine($"Your current location is now {CurrentX}, {CurrentY}.");    
                        }                        
                        
                        break;                    
                }
            }
        }

        public void MakeVisible()
        {
            CanIMove(CurrentX, CurrentY);
            CanIMove(CurrentX + 1, CurrentY);
            CanIMove(CurrentX, CurrentY + 1);
            CanIMove(CurrentX, CurrentY - 1);
            CanIMove(CurrentX -1, CurrentY);

            CanIMove(CurrentX + 1, CurrentY + 1) ;
            CanIMove(CurrentX -1, CurrentY + 1);
            CanIMove(CurrentX -1, CurrentY - 1);
            CanIMove(CurrentX + 1, CurrentY - 1);

            CanIMove(CurrentX + 2, CurrentY + 2);
            CanIMove(CurrentX - 2, CurrentY + 2);
            CanIMove(CurrentX - 2, CurrentY - 2);
            CanIMove(CurrentX + 2, CurrentY - 2);

            CanIMove(CurrentX + 2, CurrentY);
            CanIMove(CurrentX, CurrentY + 2);
            CanIMove(CurrentX, CurrentY - 2);
            CanIMove(CurrentX - 2, CurrentY);
        }

        public bool CanIMove(int x, int y)
        {
            if(x < 0 || x > Map.GetLength(0) - 1 || y < 0 || y > Map.GetLength(0) - 1)
            {
                return false;
            }
            var mapItem = Map[x, y];
            if (mapItem == null)
                return false;
            mapItem.Visible = true;
            return mapItem is Ground || mapItem is Road || mapItem is LocationSpot;
        }

        public MapItem GetMapItem(int x, int y)
        {
            if (x < 0 || x > Map.GetLength(0) - 1 || y < 0 || y > Map.GetLength(0) - 1)
            {
                return null;
            }
            return Map[x, y];            
        }

        public void Save(bool writeToConsole = true)
        {
            Database.SaveWorld(this);
            if(writeToConsole)
                WriteLineColor("Your game has been saved...", ConsoleColor.Gray, ConsoleColor.Black);
        }

        public void ProcessRandomEvent()
        {
            if(Percent(50)) // 100 / 2 = 50%
            {
                if(Percent(25)) // 100 / 4 = 25%
                {
                    CurrentForestEvent = ForestEvent.Battle;
                    var Enemies = new Humanoid();

                    Enemies.Gender = (Gender)random.Next(2);
                    Enemies.Race = (Race)random.Next(3);
                    
                    Enemies.SetLevel(MainCharacter.CombatLevel.Value);
                    if(Percent(50))
                    {
                        Enemies.Hands = WeaponItem.Clone(Database.Weapons[random.Next(Database.Weapons.Count)]);
                    }

                    WriteLineColor($"You are under attack by a {Enemies.Gender} {Enemies.Race} level {Enemies.CombatLevel.Value}!!!!", ConsoleColor.Red, ConsoleColor.Black);
                    var power = Enemies.GetWearablePower();
                    WriteLineColor($"this enemy has {power.Strength} strength {power.Defence} defence and {power.Inteligence} inteligence", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    ProcessAttackScreen(Enemies); 
                    if (CurrentLocation == Location.Forest)
                    {
                        WriteLine($"Your current location is now {CurrentX}, {CurrentY}.");
                    }
                    CurrentForestEvent = ForestEvent.None;
                    stats.UpdateWorld(this);
                }
                else if (Percent(10))
                {                        
                    var orens = random.Next(30, 101);

                    MainCharacter.Orens += orens;
                    WriteLineColor($"You found {GetOrenLabel(orens)} on the ground!!!", ConsoleColor.Yellow, ConsoleColor.Black);
                    WriteLineColor($"You now have {GetOrenLabel(MainCharacter.Orens)}", ConsoleColor.Yellow, ConsoleColor.Black);                    
                } else if (Percent(10))
                {
                    CurrentForestEvent = ForestEvent.Stranger;
                    var person = new Humanoid();
                    person.Gender = (Gender)random.Next(2);
                    person.Race = (Race)random.Next(3);
                    person.SetRandomNewName();
                    person.SetLevel(random.Next(MainCharacter.CombatLevel.Value + 5));
                    stats.UpdateWorld(this);
                    WriteLineColor($"Stranger approachers...\r\n i'm looking for somewhere to live", ConsoleColor.Red, ConsoleColor.Black);
                    string input = Question("Do you want to help this stranger?", "no","yes");
                    if (input == "yes")
                    {
                        TotalStrangersCollected += 1;

                        WriteLine($"Sure you can come to my town {HomeTown.Name}.");
                        WriteLine($"The Coordinates are {HomeTown.X}, {HomeTown.Y}.");
                        int xp = 100;

                        if (Percent(15))
                        {
                            person.Smithing.AddXP(person.Smithing.GetXPToLevelUp(RandomNumber(30)));

                            if (person.Smithing.Value > 15)
                            {
                                HomeTown.Buildings.Add(new Buildings() { Owner = person, Type = BuildingType.Blacksmith });

                                WriteLineColor($"{person.Name}, Just opened a new Blacksmith at your home town.", ConsoleColor.Yellow, ConsoleColor.Black);
                                xp += 100;
                            }
                        }
                        else if (Percent(15))
                        {
                            person.Barter.AddXP(person.Barter.GetXPToLevelUp(RandomNumber(30)));

                            if(person.Barter.Value > 10)
                            {
                                HomeTown.Buildings.Add(new Buildings() { Owner = person, Type = BuildingType.Store });

                                WriteLineColor($"{person.Name}, Just opened a new store at your home town.", ConsoleColor.Yellow, ConsoleColor.Black);
                                xp += 100;
                            }
                        }

                        WriteLineColor($"{person.Name}: Well i better get going then, Thank you so much, Catch you soon.", ConsoleColor.Yellow, ConsoleColor.Black);

                        int orens = RandomNumber(50);
                        if(orens > 0)
                        {
                            MainCharacter.Orens += orens;

                            WriteLineColor($"Hope this helps you, {person.Name} gave you {GetOrenLabel(orens)}", ConsoleColor.Yellow, ConsoleColor.Black);
                            WriteLineColor($"You now have {GetOrenLabel(MainCharacter.Orens)}", ConsoleColor.Yellow, ConsoleColor.Black);

                            UpdateStats(MainCharacter);

                            xp += 10;
                        }
                        
                        MainCharacter.Barter.AddXP(xp);

                        WriteLineColor($"You received {xp} xp for Barter Skill.", ConsoleColor.DarkCyan, ConsoleColor.Black);

                        HomeTown.PeopleWhoLiveHere.Add(person);                        
                    }
                    else
                    {
                        int orens = 100;

                        MainCharacter.Orens += orens;
                        
                        WriteLine("I'm a bandit, give me all your orens and you wont get hurt!");
                        WriteLineColor($"You stole {GetOrenLabel(orens)}!!!", ConsoleColor.Yellow, ConsoleColor.Black);
                        WriteLineColor($"You now have {GetOrenLabel(MainCharacter.Orens)}", ConsoleColor.Yellow, ConsoleColor.Black);

                        UpdateStats(MainCharacter);
                    }
                    CurrentForestEvent = ForestEvent.None;
                    stats.UpdateWorld(this);
                    WriteLine($"Your current location is now {CurrentX}, {CurrentY}.");
                }
            }
            UpdateStats(MainCharacter);
        }

        private bool wasPrevAttackPhysical = true;
        
        public bool GetMainCharacterAttackAnswer(out int damage)
        {
            string input = Question(
@"What would you like to do?
Attack the enemy.
Try to Flee.",
"flee", "attack");
            if (input == "attack")
                input = wasPrevAttackPhysical ? "physical" : "magic";

            damage = 0;

            switch (input)
            {
                case "flee":
                    if (Percent(25))
                    {
                        WriteLine("fleeing was sucessful");
                        return true;
                    }
                    else
                    {
                        WriteLine("fleeing was unsucessful");
                    }

                    break;
                case "physical":
                    wasPrevAttackPhysical = true;

                    damage = MainCharacter.GetAttackDamage();
                    break;
                case "magic":
                    wasPrevAttackPhysical = false;

                    damage = MainCharacter.GetMagicDamage();

                    break;
            }
            return false;
        }

        public static string GetOrenLabel(int value)
        {
            return value == 1 ? $"{value} oren" : $"{value} oren's";            
        }

        public void AttackMainPlayWithEnemy(Humanoid Enemy)
        {
            var power = Enemy.GetWearablePower();
            var damage = 0;

            if (power.Strength > power.Inteligence)
            {
                damage = Enemy.GetAttackDamage();                                
            }
            else
            {
                damage = Enemy.GetMagicDamage();                
            }
            Enemy.AttackOther(MainCharacter, damage);
        }

        public void ProcessAttackScreen(Humanoid Enemy)
        {
            stats.UpdateWorld(this);
            while (Enemy.IsAlive() && MainCharacter.IsAlive())
            {
                Humanoid FirstAttacker = MainCharacter.GetWearablePower().Defence <= Enemy.GetWearablePower().Defence ? MainCharacter : Enemy;
                Humanoid SecondAttacker = FirstAttacker == MainCharacter ? Enemy : MainCharacter;

                int damage = 0;

                if (FirstAttacker == MainCharacter)
                {                    
                    if(GetMainCharacterAttackAnswer(out damage))
                    {
                        return;
                    }
                    else
                    {                        
                        MainCharacter.AttackOther(Enemy, damage);                        
                    }
                    if (Enemy.IsAlive())
                    {
                        AttackMainPlayWithEnemy(Enemy);
                    }
                }
                else
                {
                    AttackMainPlayWithEnemy(Enemy);

                    if (MainCharacter.IsAlive())
                    {
                        if (GetMainCharacterAttackAnswer(out damage))
                        {
                            return;
                        }
                        else
                        {
                            MainCharacter.AttackOther(Enemy, damage);
                        }
                    }                    
                }                               
            }

            double distance = (new System.Windows.Point(CurrentX, CurrentY) - new System.Windows.Point(HomeTown.X, HomeTown.Y)).LengthSquared;
            if (distance < 0)
                distance = -distance;

            int Income = (int)(distance * 0.25d);

            if (Enemy.IsAlive())
            {                
                if(Income > 0)
                {
                    MainCharacter.Orens -= Income;

                    WriteLineColor($"You have been defeated in battle.", ConsoleColor.DarkMagenta, ConsoleColor.Black);
                    WriteLineColor($"You woke in your bed.", ConsoleColor.DarkMagenta, ConsoleColor.Black);
                    WriteLineColor($"{GetOrenLabel(Income)} have been deducted from your bag...", ConsoleColor.DarkMagenta, ConsoleColor.Black);
                    WriteLineColor($"You now have {GetOrenLabel(MainCharacter.Orens)}", ConsoleColor.Yellow, ConsoleColor.Black);                    
                }
                CurrentLocation = Location.LocationSpot;
                CurrentX = HomeTown.X;
                CurrentY = HomeTown.Y;
                ActiveLocation = HomeTown;

                ActiveWorld.IncrementTime(12, false);
            }
            else
            {
                WriteLineColor($"You have defeated your enemy in battle!", ConsoleColor.DarkMagenta, ConsoleColor.Black);

                switch (Enemy.Race)
                {
                    case Race.Orc:
                        TotalOrcsDefeated += 1;
                        break;
                    case Race.Human:
                        TotalHumansDefeated += 1;
                        break;
                    case Race.Elf:
                        TotalElfsDefeated += 1;
                        break;
                    default:
                        break;
                }

                if (Income > 0)
                {
                    MainCharacter.Orens += Income;
                    
                    WriteLineColor($"You found {GetOrenLabel(Income)} while searching the enemies body!!!", ConsoleColor.Yellow, ConsoleColor.Black);
                    
                    if (Enemy.Hands != null &&  Percent(50))
                    {
                        WriteLineColor($"You have found {Enemy.Hands.Name}.", ConsoleColor.Yellow, ConsoleColor.Black);
                        MainCharacter.Inventory.Items.Add(WeaponItem.Clone(Enemy.Hands));
                    }
                    
                    WriteLineColor($"You now have {GetOrenLabel(MainCharacter.Orens)}", ConsoleColor.Yellow, ConsoleColor.Black);                    
                }

                ActiveWorld.IncrementTime(1, false);
            }
        }
	}   
    public enum ForestEvent
    {
        None,
        Stranger,
        Battle
    }

    public enum Location
    {
        LocationSpot,
        Building,
        Forest
    }
}
