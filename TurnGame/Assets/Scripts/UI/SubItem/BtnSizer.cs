using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnSizer : UIBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //변수 목록 ========================================================================
    #region variables
    public enum actings
    {
        Atk,
        Skill,
        Order,
        Heal
    }
    public actings  _btnAct;
    GameObject      _player;
    int             _upperSortingLayer = 100;
    int             _originSorting;
    float           _outlineValue = 2f;
    float           _scaleSize = 1.2f;
    Canvas          _canvas;
    Outline         _outline;
    RectTransform   _rect;
    #endregion

    //유니티 기본 함수 ========================================================================
    #region unity functions
    void Start()
    {
        Init();
    }
    #endregion

    //초기화 ========================================================================================
    #region Init
    public override void Init()
    {
        _player = Managers.Game.GetPlayer();
        _outline = GetComponent<Outline>();
        _rect = GetComponent<RectTransform>();
        _canvas = Util.GetOrAddComponent<Canvas>(gameObject);
        _canvas.overrideSorting = true;
        _originSorting = _canvas.sortingOrder;

        Util.GetOrAddComponent<GraphicRaycaster>(gameObject);
    }
    #endregion

    //마우스, 버튼 시각적 상호작용 ========================================================================================
    #region visualizer

    //마우스 상호작용
    public void OnPointerEnter(PointerEventData eventData)
    {
        SizeController(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SizeController(false);
    }

    //사이즈 조절
    public void SizeController(bool bigger)
    {
        if (bigger)
        {
            _outline.effectDistance = new Vector2(_outlineValue, -_outlineValue);
            _rect.localScale = Vector3.one * _scaleSize;
            _canvas.sortingOrder = _upperSortingLayer;
        }
        else
        {
            _outline.effectDistance = Vector2.zero;
            _rect.localScale = Vector3.one;
            _canvas.sortingOrder = _originSorting;
        }
    }
    #endregion

    //실제 버튼동작 ========================================================================================
    #region button logic
    public void OnPointerClick(PointerEventData eventData)
    {
        BtnLogic();
    }

    public void BtnLogic()
    {
        switch (_btnAct)
        {
            case actings.Atk:
                Debug.Log("공격");
                break;
            case actings.Skill:
                Debug.Log("스킬");
                break;
            case actings.Order:
                Debug.Log("지휘");
                break;
            case actings.Heal:
                Debug.Log("힐");
                break;
        }
    }
    #endregion

}