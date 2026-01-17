using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    //변수 =====================================================================================
    #region variables
    public Define.GameState     GameState { private set; get; } = Define.GameState.Play;
    GameObject                  _player;
    HashSet<GameObject>         _monsters = new HashSet<GameObject>();
    HashSet<GameObject>         _units = new HashSet<GameObject>();

    public Action<int> MobSpawnEvent;
    public Action<int> UnitSpawnEvent;
    #endregion

    //외부 접근 함수 =====================================================================================
    #region get & set
    public GameObject GetPlayer() { return _player; }
    public GameObject TestGetPlayer() { return GameObject.FindGameObjectWithTag("Player"); }
    #endregion

    //일시정지, 해제 =====================================================================================
    #region pause
    public void ChangeGameState(Define.GameState changingState)
    {
        if(GameState == changingState)
        {
            Debug.Log($"state is already {changingState}");
            return;
        }

        if(changingState == Define.GameState.Play)
        {
            Time.timeScale = 1;
            GameState = changingState;
        }
        else
        {
            Time.timeScale = 0;
            GameState = changingState;
            Managers.UI.ShowPopUpUI<GameEndPopUp>();            
        }
    }
    #endregion

    //스폰 함수들 =====================================================================================
    #region spawn

    //스폰
    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                MobSpawnEvent?.Invoke(1);
                break;
            case Define.WorldObject.Unit:
                _units.Add(go);
                UnitSpawnEvent?.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;
    }

    //오브젝트 확인
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        TypeChecker type = go.GetComponent<TypeChecker>();
        if (type == null)
            return Define.WorldObject.Unknown;

        return type.worldObjectType;
    }
    
    //디스폰
    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (MobSpawnEvent != null)
                            MobSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorldObject.Unit:
                {
                    if (_units.Contains(go))
                    {
                        _units.Remove(go);
                        if (UnitSpawnEvent != null)
                            UnitSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
    #endregion
}
