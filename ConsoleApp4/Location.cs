using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class LocationSpot : MapItem
    {
        public string Name { get; set; }
       
        public List<Humanoid> PeopleWhoLiveHere = new List<Humanoid>();
        public List<Buildings> Buildings = new List<Buildings>();       
    }

    public class MapItem
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visible { get; set; } = false;
    }

    public class Ground : MapItem
    {

    }

    public class Tree : MapItem
    {
        
    }

    public class Road : MapItem
    {

    }

    public class Water : MapItem
    {

    }
    
    public class Buildings
    {
        public Humanoid Owner { get; set; }
        public BuildingType Type { get; set; } = BuildingType.House;

    }

    public enum BuildingType
    {
        House,
        Store,
        Blacksmith
    }
}
