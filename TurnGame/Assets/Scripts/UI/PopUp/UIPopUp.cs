using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUp : UIBase
{

    //Start에 직접적으로 넣는것보단 Init함수를 일일이 만드는게 더 좋은 방법.
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopUpUI()
    {
        Managers.UI.ClosePopUpUI(this);
    }
}
