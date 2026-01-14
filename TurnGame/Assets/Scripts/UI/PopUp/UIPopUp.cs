using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUp : UIBase
{
    enum Panel { BG }

    public override void Init()
    {
        base.Init();
        Managers.UI.SetCanvas(gameObject, true);

        Bind<GameObject>(typeof(Panel));
        GameObject bg = GetObject((int)Panel.BG);
        BindEvent(bg, (data) => BgClick(), Define.UIEvent.Click);
    }

    void BgClick()
    {
        BtnSound();
        ClosePopUpUI();
        Managers.Game.ChangeGameState(Define.GameState.Play);
    }

    public virtual void ClosePopUpUI()
    {
        Managers.UI.ClosePopUpUI(this);
    }
}
