using UnityEngine;
using UnityEngine.UI;

public class UIBlock : UIBase
{
    enum Images
    {
        UnitImage
    }

    public void Initialize()
    {
        Bind<Image>(typeof(Images));
        Image img = GetImage((int)Images.UnitImage);
        img.sprite = Managers.Resource.Load<Sprite>("Textures/Entiity/");
    }
}
