using UnityEngine;
// 다음 차례에 누가 공격할지, 어떤 공격을 선택할지에 대한 정보가 담김
[System.Serializable]
public class HandleTurn
{
    public string Attacker; // 공격자의 이름
    public string Type; // 대상이 적인지, 플레이어인지 구분하기 위한 변수
    public GameObject AttackersGameObject; // 공격하는 오브젝트
    public GameObject AttackersTarget; // 공격받는 오브젝트

    // 공격 관련
    
}
