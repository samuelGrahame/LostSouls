using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp4
{
    public class MapData
    {
        [JsonIgnoreAttribute]
        public Bitmap RawData { get; set; }
        public string RawDataString { get; set; }
        public List<LocationSpot> Locations { get; set; } = new List<LocationSpot>();

        // Base Color 111, 185, 66
        // Trees Color 41, 98, 40
        // water color 79, 117, 247
        // cities color 255, 15, 15
        // path, 147, 147, 147

        public MapItem[,] GetMapData()
        {
            if (RawData == null)
                return null;

            var map = new MapItem[RawData.Width, RawData.Height];

            for (int x = 0; x < RawData.Width; x++)
            {
                for (int y = 0; y < RawData.Height; y++)
                {
                    var color = RawData.GetPixel(x, y);
                    if (IsTree(color))
                    {
                        map[x, y] = new Tree() { X = x, Y = y };
                    }
                    else if (IsWater(color))
                    {
                        map[x, y] = new Water() { X = x, Y = y };
                    }
                    else if (IsCity(color))
                    {
                        bool found = false;

                        for (int i = 0; i < Locations.Count; i++)
                        {
                            if (Locations[i].X == x && Locations[i].Y == y)
                            {
                                map[x, y] = Locations[i];
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            map[x, y] = new LocationSpot() { X = x, Y = y };
                            Locations.Add((LocationSpot)map[x, y]);
                        }

                    }
                    else if (IsPath(color))
                    {
                        map[x, y] = new Road() { X = x, Y = y };
                    }
                    else
                    {
                        map[x, y] = new Ground() { X = x, Y = y };
                    }
                }
            }

            return map;
        }

        public void AssignToWorld(World world)
        {
            world.Map = GetMapData();
            world.Locations = Locations;
            world.MapRawData = RawData;
        }

        public bool IsTree(Color color)
        {
            return color == Color.FromArgb(41, 98, 40);
        }

        public bool IsWater(Color color)
        {
            return color == Color.FromArgb(79, 117, 247);
        }
        public bool IsCity(Color color)
        {
            return color == Color.FromArgb(255, 15, 15);
        }

        public bool IsPath(Color color)
        {
            return color == Color.FromArgb(147, 147, 147);
        }

        public bool IsGround(Color color)
        {
            return color == Color.FromArgb(111, 185, 66);
        }
    }
}
