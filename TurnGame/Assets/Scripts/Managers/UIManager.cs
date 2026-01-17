using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int                             _order = 10;
    Stack<UIPopUp>                  _popUpStack = new Stack<UIPopUp>();
    UIScene                         _sceneUI = null;

    //UI매니저 ===============================================================================================
    #region ui management
    //루트 생성
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    //sorting layer 조정
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = (_order);
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }

    }

    //월드스페이스 기준 UI 제작
    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    //서브 UI 제작
    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }

    //씬 UI 제작
    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    //팝업 UI 제작
    public T ShowPopUpUI<T>(string name = null) where T : UIPopUp
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/PopUp/{name}");
        T popUp = Util.GetOrAddComponent<T>(go);
        _popUpStack.Push(popUp);

        go.transform.SetParent(Root.transform);

        return popUp;
    }

    //팝업 UI 제거
    public void ClosePopUpUI(UIPopUp popUp)
    {
        if (_popUpStack.Count == 0)
            return;

        if(_popUpStack.Peek() != popUp)
        {
            Debug.Log("Close PopUp Failed");
            return;
        }

        ClosePopUpUI();
    }

    public void ClosePopUpUI()
    {
        if (_popUpStack.Count == 0)
            return;

        UIPopUp popUp = _popUpStack.Pop();
        Managers.Resource.Destroy(popUp.gameObject);
        popUp = null;

        _order--;
    }

    public void ClosePopUpUIAll()
    {
        while (_popUpStack.Count > 0)
            ClosePopUpUI();
    }
    #endregion

    //전체화면(기본화질 1920*1080) =====================================================================================
    #region screen
    public void SetScreenMode(Define.ScreenRatio ratio)
    {
        switch (ratio)
        {
            case Define.ScreenRatio.FullScreen:
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
                break;
            case Define.ScreenRatio.Hd:
                Screen.SetResolution((int)Define.ScreenRatio.HdWidth, (int)Define.ScreenRatio.HdHeight, FullScreenMode.Windowed);
                break;
            case Define.ScreenRatio.Fhd:
                Screen.SetResolution((int)Define.ScreenRatio.FHDWidth, (int)Define.ScreenRatio.HdHeight, FullScreenMode.Windowed);
                break;
            default:
                break;
        }
    }
    #endregion


    public void Clear()
    {
        ClosePopUpUIAll();
        _sceneUI = null;
    }
}
