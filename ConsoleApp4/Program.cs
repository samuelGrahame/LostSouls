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

            var input = Question("Welcome to Lost Souls, Would you like to play a (new, load, editor) game?", "new", "load", "editor");

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

                WriteLine($"The Doctor: Congratulations, Your wife has had a {newBorn.GetDescription()}!!");

                //newBorn.Gender = (gender = Question("The Doctor: Congratulations, Your wife has had a baby (girl, boy)?", "girl", "boy")).ToLower() == "boy" ? Gender.Male : Gender.Female;
                newBorn.Name = Question($"Your Wife: What would you like to name our {newBorn.GetDescription()}?");

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
                UpdateStats(newBorn);

                //WriteLine("Your Dad approaches...");

                //if (Question("Dad: Your Mum has been stabbed severly and died a little while ago.\r\n2 Weeks later...\r\n" +
                //    "Are you going to place something important at her gravestone? (yes, no)", "yes", "no") == "yes")
                //{
                //    newBorn.Power.Defence += 1;
                //    WriteLine("It's what she would've wanted.");                    
                //}
                //else
                //{
                //    newBorn.Power.Strength += 1;
                //    WriteLine("I'm not going to place something to rot with her corpes!");
                //}

                //UpdateStats(newBorn);

                //WriteLine("A Stranger approaches...");
                //if (Question($"Stranger: {newBorn.Race:G} give me your all your valueables? (yes, no)", "yes", "no") == "yes")
                //{
                //    newBorn.Power.Inteligence += 1;
                //    WriteLine("Here you go! Just don't kill me.");
                //}
                //else
                //{
                //    newBorn.Power.Defence += 1;
                //    WriteLine("Not over my dead body!");
                //}

                //UpdateStats(newBorn);

                //WriteLine("A Old Woman approaches...");

                //if (Question("Old Woman: i have lost alot of money can you help find my money? (yes, no)", "yes", "no") == "yes")
                //{
                //    newBorn.Power.Inteligence += 1;
                //    WriteLine("You've found it, Why thankyou.");
                //}
                //else                    
                //{
                //    newBorn.Power.Strength += 1;
                //    WriteLine("Find it yourself");
                //}

                //UpdateStats(newBorn);

                //WriteLine("Your Dad approaches...");

                //if (Question("Dad: Should I start finding another woman to be in our life? (yes, no)", "yes", "no") == "yes")
                //{
                //    newBorn.Power.Defence += 1;
                //    WriteLine("I think it's for the best Dad.");
                //}
                //else
                //{
                //    newBorn.Power.Strength += 1;
                //    WriteLine("Sorrow still feels my heart and i dont want us to move on.");
                //}

                //WriteLine("A Stranger approaches...");

                //UpdateStats(newBorn);

                //if (Question("Stranger:I will give you money if you steal a sword for me, will you do this for me? (yes, no)", "yes", "no") == "yes")
                //{
                //    newBorn.Power.Inteligence += 1;
                //    WriteLine("It's your lucky day");

                //    UpdateStats(newBorn);
                //}
                //else
                //{
                //    newBorn.Power.Defence += 1;
                //    WriteLine("your a crimanal and you will rot in jail.");

                //    UpdateStats(newBorn);

                //    WriteLine("A Old Woman approaches...");
                //    if (Question("Old Woman: I'm sick can you get me a doctor? (yes, no)", "yes", "no") == "yes")
                //    {
                //        newBorn.Power.Inteligence += 1;
                //        WriteLine("God bless you");
                //    }
                //    else
                //    {
                //        newBorn.Power.Strength += 1;
                //        WriteLine("(Remain Silent....)");
                //    }

                //    UpdateStats(newBorn);

                //    WriteLine("HELP! HELP! HELP!");
                //    if (Question("Will you? (yes, no)", "yes", "no") == "yes")
                //    {
                //        newBorn.Power.Defence += 1;
                //        WriteLine("My hero");
                //    }                    
                //    else
                //    {
                //        newBorn.Power.Strength += 1;
                //        WriteLine("Well your the careless type, A dark heart");
                //    }

                //    UpdateStats(newBorn);
                //}

                ActiveWorld.Play();                
            }            
        }

        private static bool _isReading;
        private static string _currentPrefix;

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

        public static string ReadLineInternal ()
        {
            ExternalResponce = "";
            _isReading = true;
            
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
                    
                    _isReading = false;
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
