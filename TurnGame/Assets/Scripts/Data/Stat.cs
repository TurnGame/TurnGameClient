using UnityEngine;

public class Stat : MonoBehaviour
{
    //스탯 모음 =====================================================================
    #region stat
    public Define.UnitNum   _unitnum;
    public int              UnitNum { get; protected set; } = -1;
    public float            Hp { get; protected set; }
    public float            CurrentHp { get; protected set; }
    public float            Mana { get; protected set; }
    public float            CurrentMana { get; protected set; }
    public float            Inteliigence { get; protected set; }
    public float            Attack { get; protected set; }
    public float            Defend { get; protected set; }
    public float            Speed { get; protected set; }
    #endregion

    //유니티 함수
    #region unity func
    protected void Start()
    {
        Init();
    }
    #endregion

    //초기화 함수 =====================================================================
    #region initialize
    virtual protected void Init()
    {
        UnitNum = (int)_unitnum;
        Data.Stat stat = Managers.Data.StatDict[UnitNum];
        Hp = stat.hp;
        CurrentHp = Hp;
        Mana = stat.mana;
        CurrentMana = Mana;
        Inteliigence = stat.inteliigence;
        Attack = stat.attack;
        Defend = stat.defend;
        Speed = stat.speed;
    }
    #endregion

    //피격함수(피격자측 발동) =====================================================================
    #region takeDamage
    public virtual void TakeDamage()
    {

    }
    #endregion

    //체력회복(본인 디폴트) =====================================================================
    #region heal
    protected virtual float Heal(float hp)
    {
        return hp;
    }
    #endregion

    //데미지 계산(?) =====================================================================
    #region dmgCalcul
    protected virtual float GetCurrentAtkPower()
    {
        return 0;
    }
    #endregion

    //벞디벞 관리 =====================================================================
    #region buff management
    protected virtual void BuffDeBuffManager()
    {

    }
    #endregion

}
