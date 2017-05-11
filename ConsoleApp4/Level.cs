using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Level
    {
        public int Value { get; set; }
        public float XP { get; set; }
        public float XPToLevelUp { get; set; }

        private static Dictionary<int, float> _getXPLevelUpCache { get; set; } = new Dictionary<int, float>();

        private Action<int> _OnLevelUp;

        public Level(Action<int> OnLevelUp = null)
        {
            _OnLevelUp = OnLevelUp;
            Value = 1;
            if (_OnLevelUp != null)
                _OnLevelUp(Value);
            XP = 0;
            XPToLevelUp = GetXPToLevelUp(Value);
        }

        public void AddXP(float xp)
        {
            XP += xp;
            while(XP >= XPToLevelUp)
            {
                Value++;
                if (_OnLevelUp != null)
                    _OnLevelUp(Value);
                XPToLevelUp = GetXPToLevelUp(Value);
            }
        }

        public float GetXPToLevelUp(int value)
        {
            if(_getXPLevelUpCache.ContainsKey(value))
            {
                return _getXPLevelUpCache[value];
            }
            float a = 0.0f;            
            for (float x = 1; x <= value; x++)
            {
                a += (x + 300.0f * ((float)Math.Pow(2.0f,x / 7.0f)));
            }
            a = (float)Math.Round(a / 4.0f);
            _getXPLevelUpCache[value] = a;
            return a;
        }        
    }
}
