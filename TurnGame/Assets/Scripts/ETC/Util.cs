using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util
{
    //어느 구현작업에서든 자주 쓰이는 유틸성 높은 코드 모음

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }


    public static GameObject FindChild(GameObject go, string name = null, bool again = false)
    {
        Transform transform = FindChild<Transform>(go, name, again);
        if (transform == null)
            return null;
        return transform.gameObject;
    }


    public static T FindChild<T>(GameObject go, string name = null, bool again = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(again == false)
        {
            for (int i=0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    //속도 체크
    public static Queue<Stat> CheckSpeed() 
    { 
        //각 개체 호출
        GameObject player = Managers.Game.GetPlayer();
        HashSet<GameObject> monsters = Managers.Game.GetMobs();
        HashSet<GameObject> units = Managers.Game.GetUnits();

        //리스트 생성
        int size = 1 + (monsters != null ? monsters.Count : 0) + (units != null ? units.Count : 0);
        List<Stat> tempStatList = new List<Stat>(size);

        #region null check
        if (player != null)
        {
            Stat playerStat = player.GetComponent<Stat>();
            if (playerStat != null)
                tempStatList.Add(playerStat);
        }

        if (monsters != null)
        {
            foreach (GameObject mob in monsters)
            {
                if (mob == null)
                    continue;
                Stat stat = mob.GetComponent<Stat>();
                if (stat != null)
                    tempStatList.Add(stat);
            }
        }

        if (units != null)
        {
            foreach (GameObject unit in units)
            {
                if (unit == null)
                    continue;
                Stat stat = unit.GetComponent<Stat>();
                if (stat != null)
                    tempStatList.Add(stat);
            }
        }
        #endregion


        tempStatList.Sort((a, b) =>
        {
            int speedCompare = b.Speed.CompareTo(a.Speed);

            if (speedCompare != 0)
                return speedCompare;

            var typeA = a.GetComponent<TypeChecker>().worldObjectType;
            var typeB = b.GetComponent<TypeChecker>().worldObjectType;

            return typeA.CompareTo(typeB);
        });

        return new Queue<Stat>(tempStatList);
    }
}
