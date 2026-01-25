using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
// JSON에서 불러온 데이터(원본)를 저장하는 용도
// StatData를 통해 리스트 형태의 데이터를 딕셔너리로 변환하여 쉽게 찾을 수 있도록 함
// 플레이어 Enemy 등 스탯 수치는 json에서 관리??
namespace Data
{
    #region Stat

    //스탯선언=================================================================================
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

    //스탯불러오기=================================================================================
    [Serializable]
    public class StatData : iLoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.unitNum, stat); // // Key: 유닛번호, Value: 스탯객체 전체

            return dict;
        }
    }
    #endregion
}
