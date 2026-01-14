using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScene : UIBase
{
    public override void Init()
    {
        base.Init();
        Managers.UI.SetCanvas(gameObject, false);
    }
}
