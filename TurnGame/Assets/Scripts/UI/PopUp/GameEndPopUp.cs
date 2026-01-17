using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPopUp : UIPopUp
{
    //열거형 & 변수 =====================================================================================
    #region enum & variables
    enum Texts
    { 
        Title,
        CloseTapText,
        ReturnText,
        EndText,
        BgmTxT,
        SETxT,
        LanguageTxT
    }
    
    enum Buttons
    {
        ReturnBtn,
        CloseTapBtn,
        EndBtn,
        LanguageBtn
    }

    enum DropDowns
    {
        ScreenDropDown
    }

    enum Sliders
    {
        BgmSlider,
        SESlider
    }

    Slider                      _bgmSlider;
    Slider                      _seSlider;
    #endregion

    //초기화 함수 =====================================================================================
    #region init
    public override void Init()
    {
        base.Init();
        BtnSound();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));
        Bind<TMP_Dropdown>(typeof(DropDowns));

        _seSlider = GetSlider((int)Sliders.SESlider);
        _bgmSlider = GetSlider((int)Sliders.BgmSlider);

        //드랍다운 설정
        TMP_Dropdown dropdown = Get<TMP_Dropdown>((int)DropDowns.ScreenDropDown);
        dropdown.ClearOptions();
        List<string> dropDownList = new List<string>();
        dropDownList.Add("Full Screen");
        dropDownList.Add("1920 * 1080p");
        dropDownList.Add("1280 * 720p");
        dropdown.AddOptions(dropDownList);

        //PlayerPrefs 읽기
        _seSlider.value = Managers.Sound.AudioSources[(int)Define.Sound.SE].volume;
        _bgmSlider.value = Managers.Sound.AudioSources[(int)Define.Sound.Bgm].volume;

        //슬라이더 이벤트 바인딩
        _bgmSlider.onValueChanged.AddListener((float value) =>
        {
            Managers.Sound.SetVolume(value, Define.Sound.Bgm);
        });
        _seSlider.onValueChanged.AddListener((float value) =>
        {
            Managers.Sound.SetVolume(value, Define.Sound.SE);
        });

        //버튼 이벤트 바인딩
        GetButton((int)Buttons.ReturnBtn).onClick.AddListener(ReturnTitle);
        GetButton((int)Buttons.CloseTapBtn).onClick.AddListener(CloseThisPopUp);
        GetButton((int)Buttons.EndBtn).onClick.AddListener(QuitGame);
        GetButton((int)Buttons.LanguageBtn).onClick.AddListener(LanguagePopUp);

        Transtlator(Managers.Data.Language);

        //드랍다운 이벤트 바인딩
        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.onValueChanged.AddListener(OnScreenSelected);
        dropdown.value = (int)Managers.Data.ScreenRatio;
        dropdown.RefreshShownValue();
    }
    #endregion

    //버튼 이벤트 모음 =====================================================================================
    #region btn events

    //스크린 비율
    void OnScreenSelected(int index)
    {
        switch (index)
        {
            case 0:
                Managers.UI.SetScreenMode(Define.ScreenRatio.FullScreen);
                break;
            case 1:
                Managers.UI.SetScreenMode(Define.ScreenRatio.Fhd);
                break;
            case 2:
                Managers.UI.SetScreenMode(Define.ScreenRatio.Hd);
                break;
        }
        Managers.Data.SaveScreenRatio(index);
    }

    //창닫기, 게임정지해제
    void CloseThisPopUp()
    {
        BtnSound();
        Managers.Game.ChangeGameState(Define.GameState.Play);
        PlayerPrefs.Save();
        ClosePopUpUI();
    }

    //게임종료
    void QuitGame()
    {
        BtnSound();
        PlayerPrefs.Save();
        Managers.UI.ShowPopUpUI<LastEndPopUp>();
    }

    //타이틀로
    void ReturnTitle()
    {
        BtnSound();
        Managers.Scene.LoadScene(Define.Scene.Title);
    }

    //언어변경
    void LanguagePopUp()
    {
        BtnSound();
        Managers.UI.ShowPopUpUI<LanguagePopUp>();
    }
    #endregion

    //글자 대응 =====================================================================================
    #region transtlator
    public override void Transtlator(Define.Language lang)
    {
        TMP_Text titleText = GetText((int)Texts.Title);
        TMP_Text returnText = GetText((int)Texts.ReturnText);
        TMP_Text gameEndText = GetText((int)Texts.EndText);
        TMP_Text closeText = GetText((int)Texts.CloseTapText);
        TMP_Text bgmTxT = GetText((int)Texts.BgmTxT);
        TMP_Text seTxT = GetText((int)Texts.SETxT);
        TMP_Text languageTxT = GetText((int)Texts.LanguageTxT);
        
        switch (lang)
        {
            case Define.Language.Korean:
                {
                    titleText.text = "일시정지";
                    returnText.text = "타이틀로";
                    gameEndText.text = "게임종료";
                    closeText.text = "창 닫기";
                    bgmTxT.text = "배경음";
                    seTxT.text = "효과음";
                    languageTxT.text = "언어";
                    break;
                }
            case Define.Language.English:
                {
                    titleText.text = "Paused";
                    returnText.text = "Return title";
                    gameEndText.text = "End game";
                    closeText.text = "Close Tap";
                    bgmTxT.text = "Bgm";
                    seTxT.text = "SE";
                    languageTxT.text = "Language";
                    break;
                }
        }
    }
    #endregion

}
