using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : UIScene
{
    Queue<Stat> _allUnitsQueue = new Queue<Stat>();

    public override void Init()
    {
        base.Init();
    }

    public void SpeedCalculator()
    {
        //시작페이즈때 호출
        if (_allUnitsQueue.Count <= 0)
        {
            _allUnitsQueue = Util.CheckSpeed();

            foreach (var unit in _allUnitsQueue) 
            {
                GameObject go = Managers.Resource.Instantiate("UI/SubItem/UIBlock");
                GameObject goImage = Util.FindChild(go, "UnitImage");
                goImage.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Textures/Entiity/{unit._unitnum}");
            }
        }
        //도중에 호출
        else
        {
            //대충 기존에 있는 큐에 끼워맞추고 이미지에도 적용
        }
            
    }
}
