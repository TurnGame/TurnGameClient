using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguagePopUp : UIPopUp
{
    //열거형 & 변수 ================================================================================================
    #region enum & variables

    enum Texts
    {
        Title,
        PopBtnTxT
    }

    enum Buttons
    {
        KoBtn,
        EnBtn,
        PopUpCloseBtn
    }
    #endregion

    //초기화 ================================================================================================
    #region init
    public override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.KoBtn).onClick.AddListener(() => LanguageSetting(Define.Language.Korean));
        GetButton((int)Buttons.EnBtn).onClick.AddListener(() => LanguageSetting(Define.Language.English));
        GetButton((int)Buttons.PopUpCloseBtn).onClick.AddListener(CloseTap);

        Transtlator(Managers.UI.Language);
    }
    #endregion

    //창 닫기 ================================================================================================
    #region closeTap
    private void CloseTap()
    {
        BtnSound();
        ClosePopUpUI();
    }
    #endregion

    //언어 설정 ================================================================================================
    #region languageSetting
    void LanguageSetting(Define.Language language)
    {
        BtnSound();
        Managers.UI.SetLanguage(language);
    }

    #endregion
    //번역 ================================================================================================
    #region transtlator
    public override void Transtlator(Define.Language lang)
    {
        TMP_Text titleTxT = GetText((int)Texts.Title);
        TMP_Text popBtnTxT = GetText((int)Texts.PopBtnTxT);

        switch (Managers.UI.Language)
        {
            case Define.Language.Korean:
                titleTxT.text = "언어설정";
                popBtnTxT.text = "창 닫기";
                break;
            case Define.Language.English:
                titleTxT.text = "Languages";
                popBtnTxT.text = "Close Tap";
                break;
        }
    }
    #endregion
}
