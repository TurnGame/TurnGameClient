using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat

    //½ºÅÈ¼±¾ð=================================================================================
    [Serializable]
    public class Stat
    {
        public int unitNum;
        public float hp;
        public float mana;
        public float critical;
        public float criticalDmg;
        public float inteliigence;
        public float attack;
        public float defend;
        public float speed;
    }

    //½ºÅÈºÒ·¯¿À±â=================================================================================
    [Serializable]
    public class StatData : iLoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.unitNum, stat);

            return dict;
        }
    }
    #endregion
}
