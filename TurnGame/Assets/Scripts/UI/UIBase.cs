using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public virtual void Init()
    {
        Managers.UI.OnLanguageChanged -= Transtlator;
        Managers.UI.OnLanguageChanged += Transtlator;
    }

    private void Start()
    {
        Init();
    }
    protected virtual void OnDisable()
    {
        if (Managers.HasInstance)
        {
            Managers.UI.OnLanguageChanged -= Transtlator;
        }
    }

    protected void BtnSound() { Managers.Sound.Play("SE/BtnSound"); }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        //매핑
        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind ({names[i]})");
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[index] as T;
    }

    protected GameObject GetObject(int index) { return Get<GameObject>(index); }

    protected TMP_Text GetText(int index) { return Get<TMP_Text>(index); }

    protected Button GetButton(int index) { return Get<Button>(index); }
    
    protected Slider GetSlider(int index) { return Get<Slider>(index); }

    protected Image GetImage(int index) { return Get<Image>(index); }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }

    //언어설정
    public virtual void Transtlator(Define.Language lang) { }
}
