using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Console;

namespace ConsoleApp4
{
    class Program
    {
        public static frmStats stats;
        public static World ActiveWorld;

        public static void UpdateStats(Humanoid livingEntity)
        {
            stats.UpdateStats(livingEntity);            
        }

        [STAThread]
        static void Main(string[] args)
        {           
            Application.EnableVisualStyles();
            
            stats = new frmStats();
            //System.IO.File.read            
            Database.LoadData();
                        
            ActiveWorld = new World();

            var input = Question("Welcome to Lost Lands, Would you like to play a (new, load, editor) game?", "new", "load", "editor");

            if (input == "load" && Database.LoadWorld(out ActiveWorld))
            {
                ActiveWorld.Play();
            }else if (input == "editor")
            {
                // hidden stuff hihih
                var thr2 = new Thread(() => {                    
                    Application.Run(new frmEditor());                    
                });
                thr2.SetApartmentState(ApartmentState.STA);
                thr2.Start();
            }
            else
            {               
                // Load Map From Bitmap
                
                var newBorn = ActiveWorld.Born(new Humanoid());

                ActiveWorld.MainCharacter = newBorn;

                Random randomizer = new Random();

                newBorn.Gender = (Gender)randomizer.Next(2);

                newBorn.Race = (Race)randomizer.Next(3);

                WriteLine($"The Doctor: Congratulations, it is a {newBorn.GetDescription()}!!");

                //newBorn.Gender = (gender = Question("The Doctor: Congratulations, Your wife has had a baby (girl, boy)?", "girl", "boy")).ToLower() == "boy" ? Gender.Male : Gender.Female;
                newBorn.Name = Question($"What would you like to name {newBorn.GetDescription()}?");

                if(string.IsNullOrWhiteSpace(newBorn.Name))
                {
                    newBorn.SetRandomNewName();
                }
                newBorn.Name = newBorn.Name.ToTitleCase();

                WriteLine($"The Doctor: {newBorn.Name.ToTitleCase()}'s a {(newBorn.Gender == Gender.Male ? "handsome" : "beautiful")} little {newBorn.Race:G}.");
                //newBorn.Race = (race = Question($"The Doctor: {newBorn.Name}'s a {(newBorn.Gender == Gender.Male ? "handsome" : "beautiful")} little ()).ToLower() == "human" ?
                //		Race.Human :
                //		race == "elf" ?
                //		Race.Elf :
                //		Race.Orc;

                //ActiveWorld.HomeTown = new LocationSpot() { Name = "Novigrad", X = randomizer.Next(2500), Y = randomizer.Next(2500) };
                //ActiveWorld.Locations.Add(ActiveWorld.HomeTown);
               
                Database.LoadMap().AssignToWorld(ActiveWorld);

                ActiveWorld.HomeTown = ActiveWorld.Locations[randomizer.Next(ActiveWorld.Locations.Count)];
                ActiveWorld.ActiveLocation = ActiveWorld.HomeTown;

                ActiveWorld.CurrentX = ActiveWorld.HomeTown.X;
                ActiveWorld.CurrentY = ActiveWorld.HomeTown.Y;

                ActiveWorld.CanIMove(ActiveWorld.CurrentX, ActiveWorld.CurrentY);
                ActiveWorld.CanIMove(ActiveWorld.CurrentX - 1, ActiveWorld.CurrentY);
                ActiveWorld.CanIMove(ActiveWorld.CurrentX, ActiveWorld.CurrentY - 1);
                ActiveWorld.CanIMove(ActiveWorld.CurrentX + 1, ActiveWorld.CurrentY);
                ActiveWorld.CanIMove(ActiveWorld.CurrentX, ActiveWorld.CurrentY + 1);

                WriteLine("________________________________");
                WriteLine("15 years later...");
                WriteLine("________________________________");

                WriteLine("You received 10 Power Points!!!!");

                newBorn.AvailablePowerPoints += 10;
                
                //UpdateStats(newBorn);
             
                //(Quest 1("Old Woman: I need some tree seeds?

                //    newBorn.Power.Defence += 1;
                //    newBorn.Power.Inteligence += 1;
                //    newBorn.Power.Strength += 1;

                //UpdateStats(newBorn);

                //(Quest 2("Old Woman: I'm hungary can you get me some fish?

                //    newBorn.Power.Defence += 1;
                //    newBorn.Power.Inteligence += 1;
                //    newBorn.Power.Strength += 1;

                //UpdateStats(newBorn);
               
                //(Quest 3("Stranger:A bounty on RandomHumanoid, 

                //    newBorn.Power.Defence += 1;
                //    newBorn.Power.Inteligence += 1;
                //    newBorn.Power.Strength += 1;

                //    UpdateStats(newBorn);
                
                //(Quest 4("Stranger:A bounty on RandomHumanoid, 
                //
                //    newBorn.Power.Defence += 1;
                //    newBorn.Power.Inteligence += 1;
                //    newBorn.Power.Strength += 1;
                
                //(Quest 5("Stranger:Steal someone's valueables?

                //    newBorn.Power.Defence += 1;
                //    newBorn.Power.Inteligence += 1;
                //    newBorn.Power.Strength += 1;

                //    UpdateStats(newBorn);
                
                //(Quest 6("Builder: I Need some wood to build? 

                //    newBorn.Power.Defence += 1;
                //    newBorn.Power.Inteligence += 1;
                //    newBorn.Power.Strength += 1;

                //    UpdateStats(newBorn);

                ActiveWorld.Play();                
            }            
        }

        private static bool _isReading;

        static void ClearConsole()
        {
            var length = Console.CursorLeft;
            Console.CursorLeft = 0;

            for (int i = 0; i <= length; i++)
            {
                Console.Write(" ");
            }
            Console.CursorLeft = 0;
        }

        private static readonly StringBuilder CurrentReadLine = new StringBuilder();

        public static string ExternalResponce = "";

        public static bool IsReading { get => _isReading; set => _isReading = value; }
        public static bool IsReading1 { get => _isReading; set => _isReading = value; }

        public static string ReadLineInternal()
        {
            ExternalResponce = "";
            IsReading = true;

            while (true)
            {
                ConsoleKeyInfo cki;

                cki = Console.ReadKey();

                if (cki.Key == ConsoleKey.Escape)
                {
                    CurrentReadLine.Clear();
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    if (CurrentReadLine.Length > 0)
                    {
                        CurrentReadLine.Length--;
                    }
                    ClearConsole();
                    Console.Write(CurrentReadLine);
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();

                    if (ExternalResponce.Length > 0)
                    {
                        CurrentReadLine.Append(ExternalResponce);
                        ExternalResponce = "";
                    }

                    var result = CurrentReadLine.ToString();
                    CurrentReadLine.Clear();
                    
                    IsReading = false;
                    return result;
                }
                else
                {
                    CurrentReadLine.Append(cki.KeyChar);
                }
            }
        }


        public static void WriteLineColor(string value, ConsoleColor ForeColor, ConsoleColor BackColor)
        {
            Console.ForegroundColor = ForeColor;
            Console.BackgroundColor = BackColor;

            WriteLine(value);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static string Question(string value, params string[] args)
        {
            bool ExitLoop;
            do
            {
                ExitLoop = true;
                
                WriteLineColor(value, ConsoleColor.Cyan, ConsoleColor.Black);
                
                var x = ReadLineInternal().ToLower();
                if (x == "clear")
                {
                    Console.Clear();
                    WriteLineColor(value, ConsoleColor.Cyan, ConsoleColor.Black);
                    x = Console.ReadLine().ToLower();
                }
                    
                
                if (args != null && args.Length > 0)
                    foreach (var item in args)
                        if (x.Contains(item.ToLower()) || item.ToLower().Contains(x))
                            return item.ToLower();                        
                ExitLoop = false;

                WriteLine("Sorry, that was not an option...");
            } while (!ExitLoop);
            return string.Empty;
        }

        public static string Question(string value)
        {
            WriteLineColor(value, ConsoleColor.Cyan, ConsoleColor.Black);

            var x = ReadLineInternal().ToLower();

            if (x == "clear")
            {
                Console.Clear();
                WriteLineColor(value, ConsoleColor.Cyan, ConsoleColor.Black);
                x = Console.ReadLine().ToLower();
            }
            return x;
        }

        public static int QuestionNumber(string value)
        {
            Console.WriteLine(value);

            string value1 = Console.ReadLine().ToLower();

            int value2 = 0;

            int.TryParse(value1, out value2);

            return value2;
        }
    }

    public static class Extensions
    {
        public static bool Contains(this string value, params string[] args)
        {
            if (args == null || args.Length == 0)
                return false;

            for (int i = 0; i < args.Length; i++)
                if (value.Contains(args[i]))
                    return true;
            return false;
        }

        private static TextInfo textinfo = null;

        public static string ToTitleCase(this string value)
        {
            if (textinfo == null)
            {
                textinfo = new CultureInfo("en-US", false).TextInfo;
            }
            return textinfo.ToTitleCase(value);
        }
    }
}
