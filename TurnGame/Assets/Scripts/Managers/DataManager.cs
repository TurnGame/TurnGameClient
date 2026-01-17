using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface iLoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    public Define.ScreenRatio ScreenRatio { get; private set; } = Define.ScreenRatio.FullScreen;
    public Define.Language Language { private set; get; } = Define.Language.Korean;
    
    public Action<Define.Language> OnLanguageChanged = null;

    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        
    }

    public void PrefInit()
    {
        GetLanguage();
        LoadScreenRatio();
        LoadVolumeSetting();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : iLoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    //PlayerPref 관리 ===============================================================================================
    #region playerpref get set

    //언어저장
    void GetLanguage()
    {
        string languageSetting = PlayerPrefs.GetString("Language", Define.Language.Korean.ToString());
        Language = (Define.Language)Enum.Parse(typeof(Define.Language), languageSetting);
    }

    //언어로드
    public void SetLanguage(Define.Language lang)
    {
        Language = lang;
        OnLanguageChanged?.Invoke(lang);
        PlayerPrefs.SetString("Language", lang.ToString());
    }

    //볼륨저장
    public void LoadVolumeSetting()
    {
        Managers.Sound.GetAudio(Define.Sound.Bgm).volume = PlayerPrefs.GetFloat(Define.Sound.Bgm.ToString(), 1.0f);
        Managers.Sound.GetAudio(Define.Sound.SE).volume = PlayerPrefs.GetFloat(Define.Sound.SE.ToString(), 1.0f);
    }

    //볼륨로드
    public void SaveVolumeSetting(Define.Sound volume)
    {
        switch (volume)
        {
            case Define.Sound.Bgm:
                PlayerPrefs.SetFloat(Define.Sound.Bgm.ToString(), Managers.Sound.GetAudio(Define.Sound.Bgm).volume);
                break;
            case Define.Sound.SE:
                PlayerPrefs.SetFloat(Define.Sound.SE.ToString(), Managers.Sound.GetAudio(Define.Sound.SE).volume);
                break;
        }
        
    }

    //비율저장
    public void SaveScreenRatio(int index)
    {
        if (index > 2)
        {
            Debug.Log($"Wrong Value Choice : index is {index}");
            return;
        }
        PlayerPrefs.SetInt("Screen", index);
        ScreenRatio = (Define.ScreenRatio)index;
    }

    //비율 불러오기
    public void LoadScreenRatio()
    {
        int screenNum = PlayerPrefs.GetInt("Screen", (int)Define.ScreenRatio.FullScreen);
        ScreenRatio = (Define.ScreenRatio)screenNum;
    }
    #endregion

    public void Clear()
    {
        OnLanguageChanged = null;
    }
    
}
