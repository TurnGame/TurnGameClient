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

    enum Sliders
    {
        BgmSlider,
        SESlider
    }

    Slider  _bgmSlider;
    Slider  _seSlider;

    #endregion

    //유니티 함수 =====================================================================================
    #region unity func
    protected override void OnDisable() 
    { 
        base.OnDisable();
        if(Managers.HasInstance) Managers.Sound.SaveVolumeSetting(); 
    }
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

        _seSlider = GetSlider((int)Sliders.SESlider);
        _bgmSlider = GetSlider((int)Sliders.BgmSlider);

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

        Transtlator(Managers.UI.Language);
    }
    #endregion

    //버튼 이벤트 모음 =====================================================================================
    #region btn events

    //창닫기, 게임정지해제
    void CloseThisPopUp()
    {
        BtnSound();
        Managers.Game.ChangeGameState(Define.GameState.Play);
        ClosePopUpUI();
    }

    //게임종료
    void QuitGame()
    {
        BtnSound();
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
    //3. 깃허브 업로드하고 풀리퀘 강의듣기
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

        switch (Managers.UI.Language)
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
