using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp4
{
	public static class Database
	{
        /*
		 * sword
		 * mace
		 * axe
		 * battle axe
		 * greatsword
		 * warhammer
		 * firebolt
		 * frost spike
		 * lightning bolt
		 * flame
		 * frostbite
		 * shock
		 */


        private static readonly JsonSerializerSettings DefaultJsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            DateFormatString = "yyyy-MM-dd",
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, //prevents a infinite loop of serialisation
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full //FIXES ISSUE WHERE JSON HAS NO IDEA WHAT TO DO WITH DYNAMIC PROXIES GENERATED FROM EF
        }; // able to link to DLL

        public static string PackObject(object source)
        {
            return JsonConvert.SerializeObject(source, DefaultJsonSettings);
        }

        public static T Unpack<T>(string source)
        {
            return JsonConvert.DeserializeObject<T>(source, DefaultJsonSettings);
        }

        public static string ImageToBase65(Image image)
        {
            if (image == null)
                return string.Empty;

            using (MemoryStream mem = new MemoryStream())
            {
                image.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(mem.ToArray());
            }
        }

        public static Image Base64ToImage(string str)
        {
            if (str == string.Empty)
                return null;

            using (MemoryStream mem = new MemoryStream(Convert.FromBase64String(str)))
            {
                return Image.FromStream(mem);
            }
        }
        

    //Public Function Base64ToImage(ByVal nString As String) As Image
    //    If nString = "" Then Return Nothing
    //    Dim buffer = Convert.FromBase64String(nString)
    //    Dim Mem As New MemoryStream(buffer)

    //    Return Image.FromStream(Mem)
    //End Function

        public static MapData LoadMap()
        {
            if (File.Exists("Database/Map/map.json"))
            {
                var mapdata = Unpack<MapData>(File.ReadAllText("Database/Map/map.json"));
                mapdata.RawData = Base64ToImage(mapdata.RawDataString) as Bitmap;

                return mapdata;
            }
            return null;
        }

        public static void SaveMap(MapData mapdata)
        {
            if (!Directory.Exists("Database/Map/"))
            {
                Directory.CreateDirectory("Database/Map/");
            }
            mapdata.RawDataString = ImageToBase65(mapdata.RawData);
            File.WriteAllText("Database/Map/map.json", PackObject(mapdata));            
        }
        
        public static void SaveWorld(World world)
        {
            if (!Directory.Exists("Database/"))
            {
                Directory.CreateDirectory("Database/");
            }

            if (!Directory.Exists("Database/Saves/"))
            {
                Directory.CreateDirectory("Database/Saves/");
            }
            world.MapRawDataString = ImageToBase65(world.MapRawData);
            File.WriteAllText("Database/Saves/save.json", PackObject(world));            
        }

        public static bool LoadWorld(out World world)
        {
            world = new World();

            if (Directory.Exists("Database/"))
            {
                if (Directory.Exists("Database/Saves/"))
                {
                    try
                    {
                        world = Unpack<World>(File.ReadAllText("Database/Saves/save.json"));
                        world.MapRawData = Base64ToImage(world.MapRawDataString) as Bitmap;
                        return true;
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }

            return false;
        }
        
        public static void CreateDirectoryIfNotExist(string path)
        {
            if (!Directory.Exists(path))            
                Directory.CreateDirectory(path);            
        }

        public static void SaveWearable(WearableItem item, string filePath)
        {
            try
            {
                File.WriteAllText(filePath + item.Name + ".csv", $"{item.Name},{item.Description},{item.Power.Strength},{item.Power.Inteligence},{item.Power.Defence}");
            }
            catch (Exception)
            {

            }
            
        }

        public static void ClearDirectory(string filePath)
        {
            foreach (var file in Directory.GetFiles(filePath))
            {
                File.Delete(file);
            }
        }

        public static void SaveData()
        {
            CreateDirectoryIfNotExist("Database/");
            CreateDirectoryIfNotExist("Database/Weapons/");
            ClearDirectory("Database/Weapons/");
            CreateDirectoryIfNotExist("Database/HeadWear/");
            ClearDirectory("Database/HeadWear/");
            CreateDirectoryIfNotExist("Database/LegWear/");
            ClearDirectory("Database/LegWear/");
            CreateDirectoryIfNotExist("Database/FeetWear/");
            ClearDirectory("Database/FeetWear/");
            CreateDirectoryIfNotExist("Database/ChestWear/");
            ClearDirectory("Database/ChestWear/");



            foreach (var weapon in Weapons)
            {
                SaveWearable(weapon, "Database/Weapons/");
            }
            foreach (var headwear in HeadWear)
            {
                SaveWearable(headwear, "Database/HeadWear/");
            }
            foreach (var legWear in LegWear)
            {
                SaveWearable(legWear, "Database/LegWear/");
            }
            foreach (var feetWear in FeetWear)
            {
                SaveWearable(feetWear, "Database/FeetWear/");
            }
            foreach (var chestWear in ChestWear)
            {
                SaveWearable(chestWear, "Database/ChestWear/");
            }

        }

        public static void LoadData()
		{
            CreateDirectoryIfNotExist("Database/");
            CreateDirectoryIfNotExist("Database/Weapons/");
            CreateDirectoryIfNotExist("Database/HeadWear/");
            CreateDirectoryIfNotExist("Database/LegWear/");
            CreateDirectoryIfNotExist("Database/FeetWear/");
            CreateDirectoryIfNotExist("Database/ChestWear/");
            CreateDirectoryIfNotExist("Database/Map/");
            
            foreach (var filePath in Directory.GetFiles("Database/Weapons/"))
            {
                Database.LoadWearable(filePath, Weapons, new WeaponItem());
            }
            foreach (var filePath in Directory.GetFiles("Database/HeadWear/"))
            {
                Database.LoadWearable(filePath, HeadWear, new HeadItem());
            }
            foreach (var filePath in Directory.GetFiles("Database/LegWear/"))
            {
                Database.LoadWearable(filePath, LegWear, new LegsItem());
            }
            foreach (var filePath in Directory.GetFiles("Database/FeetWear/"))
            {
                Database.LoadWearable(filePath, FeetWear, new FeetItem());
            }
            foreach (var filePath in Directory.GetFiles("Database/ChestWear/"))
            {
                Database.LoadWearable(filePath, ChestWear, new ChestItem());
            }
		}
        
        public static void LoadWearable(string filePath, IList list, WearableItem item)
        {
            string fileData = File.ReadAllText(filePath);
            string[] splitData = fileData.Split(',');

            if (splitData.Length == 5)
            {
                item.Name = splitData[0];
                item.Description = splitData[1];
                item.Power = new Ability() { Strength = int.Parse(splitData[2]), Inteligence = int.Parse(splitData[3]), Defence = int.Parse(splitData[4]) };
                list.Add(item);
            }
        }

        public static List<WeaponItem> Weapons = new List<WeaponItem>();
        public static List<HeadItem> HeadWear = new List<HeadItem>();
        public static List<LegsItem> LegWear = new List<LegsItem>();
        public static List<FeetItem> FeetWear = new List<FeetItem>();
        public static List<ChestItem> ChestWear = new List<ChestItem>();
    }
}
