using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : UIScene
{
    //열거형
    #region enums
    enum Texts
    {
        Title,
        StartTxT,
        SetTxT,
        EndTxT
    }

    enum Buttons
    {
        StartBtn,
        EndBtn,
        SetBtn
    }
    #endregion

    public override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.StartBtn).onClick.AddListener(GameStart);
        GetButton((int)Buttons.SetBtn).onClick.AddListener(OpenSetting);
        GetButton((int)Buttons.EndBtn).onClick.AddListener(EndGame);

        Transtlator(Managers.Data.Language);
    }

    async void GameStart()
    {
        GetButton((int)Buttons.StartBtn).interactable = false;
        BtnSound();
        await Task.Delay(200);
        Managers.Scene.LoadScene(Define.Scene.Stage1);
    }

    void OpenSetting()
    {
        BtnSound();
        Managers.UI.ShowPopUpUI<TitlePopUp>();
    }

    void EndGame()
    {
        BtnSound();
        Managers.UI.ShowPopUpUI<LastEndPopUp>();
    }
    // 1. 타이틀 화면에서 esc, 설정 관련 ui 누를때 찐빠 고쳐야함
    public override void Transtlator(Define.Language lang)
    {
        TMP_Text startTxT = GetText((int)Texts.StartTxT);
        TMP_Text endTxT = GetText((int)Texts.EndTxT);
        TMP_Text setTxT = GetText((int)Texts.SetTxT);

        switch (lang)
        {
            case Define.Language.Korean:
                {
                    startTxT.text = "게임시작";
                    endTxT.text = "게임종료";
                    setTxT.text = "환경설정";
                }
                break;
            case Define.Language.English:
                {
                    startTxT.text = "Game Start";
                    endTxT.text = "Quit Game";
                    setTxT.text = "Setting";
                }
                break;
        }
    }
}
