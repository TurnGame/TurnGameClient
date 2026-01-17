using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LastEndPopUp : UIPopUp
{
    //열거형 & 변수 ================================================================================================
    #region enum & variables

    enum Texts
    {
        Title,
        YesTxT,
        NoTxT,
    }

    enum Buttons
    {
        YesBtn,
        NoBtn
    }
    #endregion

    //초기화 ================================================================================================
    #region init
    public override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.NoBtn).onClick.AddListener(CloseTap);
        GetButton((int)Buttons.YesBtn).onClick.AddListener(QuitGame);

        Transtlator(Managers.Data.Language);
    }
    #endregion

    //버튼 이벤트 
    #region buttons
    
    //아니오버튼
    void CloseTap()
    {
        BtnSound();
        ClosePopUpUI();
    }

    //예 버튼
    void QuitGame()
    {
        BtnSound();
        PlayerPrefs.Save();
        Application.Quit();
    }
    #endregion

    //번역 ================================================================================================
    #region transtlator
    public override void Transtlator(Define.Language lang)
    {
        TMP_Text titleTxT = GetText((int)Texts.Title);
        TMP_Text yesTxT = GetText((int)Texts.YesTxT);
        TMP_Text noTxT = GetText((int)Texts.NoTxT);

        switch (lang)
        {
            case Define.Language.Korean:
                titleTxT.text = "정말 게임을 종료 하시겠습니까?";
                yesTxT.text = "예";
                noTxT.text = "아니오";
                break;
            case Define.Language.English:
                titleTxT.text = "Quit Game?";
                yesTxT.text = "Yes";
                noTxT.text = "No";
                break;
        }
    }
    #endregion
}
