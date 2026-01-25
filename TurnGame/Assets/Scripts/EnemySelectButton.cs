/*using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab; // BST에서 게임 오브젝트를 전달해야 함
    public void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMaschine>().Input2(EnemyPrefab); // Enemy버튼 클릭시 SelectEnemy()가 호출되고, SelectEnemy에서 BSM의 Input2()호출함
    }



    public void HideSelector()
    {
        EnemyPrefab.transform.Find("Selector").gameObject.SetActive(false);
    }

    // 마우스 커서를 Enemy 버튼에 갖다대면 Selector가 표시되도록 하는 함수(Target1 Button에 Event Trigger 컴포넌트를 추가했음)
    public void ShowSelector()
    {
        EnemyPrefab.transform.Find("Selector").gameObject.SetActive(true);
    }
}
*/