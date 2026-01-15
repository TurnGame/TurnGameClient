using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActorUI : UIScene
{
    //변수목록 ==========================================================================================================
    #region variables
    enum Images
    {
        DownBtn,
        UpBtn,
        RightBtn,
        LeftBtn
    }
    private BtnSizer    _lastActiveBtn = null;
    private KeyCode     _lastKeyCode = KeyCode.None;
    #endregion

    //초기화 함수 ==========================================================================================================
    #region Init
    public override void Init() 
    { 
        base.Init();

        Bind<Image>(typeof(Images));

        Managers.UI.OnLanguageChanged -= Transtlator;
        Managers.Input.KeyDownAction -= OnKeyDown;
        Managers.Input.KeyUpAction -= OnKeyUp;
        Managers.Input.KeyDownAction += OnKeyDown;
        Managers.Input.KeyUpAction += OnKeyUp;
    }
    #endregion

    //키 체크 ==========================================================================================================
    #region keyChecker
    void OnKeyDown(KeyCode keyCode)
    {
        BtnSizer newBtn = GetBtnSizerByKey(keyCode);

        if (newBtn == null) return;
        if (_lastActiveBtn != null)
        {
            _lastActiveBtn.SizeController(false);
        }

        newBtn.SizeController(true);

        _lastActiveBtn = newBtn;
        _lastKeyCode = keyCode;
    }

    void OnKeyUp(KeyCode keyCode)
    {
        if (_lastKeyCode == keyCode)
        {
            if (_lastActiveBtn != null)
            {
                BtnSizer btn = GetBtnSizerByKey(keyCode);
                btn.BtnLogic();

                _lastActiveBtn.SizeController(false);
                _lastActiveBtn = null;
                _lastKeyCode = KeyCode.None;
            }
        }
    }

    BtnSizer GetBtnSizerByKey(KeyCode key)
    {
        int index = -1;
        switch (key)
        {
            case KeyCode.W: index = (int)Images.UpBtn; break;
            case KeyCode.A: index = (int)Images.LeftBtn; break;
            case KeyCode.S: index = (int)Images.DownBtn; break;
            case KeyCode.D: index = (int)Images.RightBtn; break;
        }

        if (index == -1) return null;
        return GetImage(index).GetComponent<BtnSizer>();
    }

    #endregion


}
