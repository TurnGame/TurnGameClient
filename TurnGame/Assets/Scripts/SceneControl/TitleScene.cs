using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Title;
        Managers.UI.ShowSceneUI<TitleUI>();
    }

    public override void Clear()
    {

    }
}
