using UnityEngine;

public class Stage1Scene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Stage1;

        Managers.UI.ShowSceneUI<ActorUI>();
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
