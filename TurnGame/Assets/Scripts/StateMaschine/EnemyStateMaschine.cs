using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateMaschine : MonoBehaviour
{
    private BattleStateMaschine BSM;
    public enum TurnState
    {
        PROCESSING, // 턴 처리 상태
        CHOOSEACTION, // 선택 상태
        WAITING, // 대기 중 상태
        ACTION, // 행동
        DEAD // 죽음
    }

    public TurnState currentstate;

    // 진행률 표시 줄에 사용
    private float cur_cooldown = 0f;
    private float max_cooldown = 8f; // 적 공격 쿨타임으로 생각해도 됨
    // this gameobject
    private Vector3 startposition;
    public GameObject Selector;
    // 액션 시간 정보 (timeforaction stuff)
    private bool actionStarted = false;
    public GameObject HeroToAttack; // BST에서 지정해줌
    private float animSpeed = 10f;

    void Start()
    {
        currentstate = TurnState.PROCESSING; // 게임 시작하고 바로 턴 처리 상태가 활성화
        Selector.SetActive(false);
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMaschine>(); // BattleStateMaschine과 연결
        startposition = transform.position; // 시작 위치
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentstate);
        switch (currentstate)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;
            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentstate = TurnState.WAITING;
                break; ;
            case (TurnState.WAITING):
                // idle state
                break; ;
            case (TurnState.ACTION): // 나중에 BSM에 의해 설정됨
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):

                break;
        }
    }
    void UpgradeProgressBar() // 진행률 표시줄 업그레이드
    {
        cur_cooldown = cur_cooldown + Time.deltaTime; // 쿨다운 시간이 증가

        if (cur_cooldown >= max_cooldown)
        {
            currentstate = TurnState.CHOOSEACTION; // 쿨다운이 최대치인 5초를 초과하면 상태를 변경
        }

    }

    void ChooseAction() // 적의 자동 행동 선택
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = name; // 공격자 이름
        myAttack.Type = "Enemy"; // 쿨타운이 끝나면 TurnState.CHOOSEACTION로 상태 전환하고, TurnState.CHOOSEACTION에서 ChooseAction()를 호출하여 myAttack(후에 PerformList).type이 "Enemy"로 설정됨
        myAttack.AttackersGameObject = this.gameObject; // 공격하는 오브젝트
        myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)]; // 공격 대상 (HerosInBattle에서 랜덤으로 선택)
        BSM.CollectActions(myAttack); // myAttack 매개변수를 통해서 공격자 이름, 오브젝트 등의 정보를 BattleStateMaschine에 제공함
    }

    private IEnumerator TimeForAction() // 시간 제한 행동 (행동을 시간 순서대로 수행하도록 함)
    {
        if (actionStarted) // 동작이 이미 실행됐다면 빠져나옴
        {
            yield break;
        }
        actionStarted = true;
        // 플레이어 근처에서 공격하도록 위치 설정
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 1.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z); // 플레이어 위치보다 살짝 앞으로 설정

        while (MoveTowardsEnemy(heroPosition))
        {
            yield return null; // 적을 향해 이동하는 동안 아무것도 하지 않고 기다림.
        }

        // 잠시 기다렸다가
        yield return new WaitForSeconds(0.5f);
        // 피해를 주고 

        // 다시 초기 위치로 돌아감
        Vector3 firstPosition = startposition;
        while (MoveTowardsStart(firstPosition)) { yield return null; }

        // BSM에서 Performer를 목록에서 제거(같은 동작을 두 번 반복하지 않도록 하기 위해)
        BSM.PerformList.RemoveAt(0);

        // BSM 초기화 (wait 상태로 전환)
        BSM.battleStates = BattleStateMaschine.PerformAction.WAIT;

        // 코루틴 종료
        actionStarted = false;

        // enemy state 초기화
        cur_cooldown = 0f;
        currentstate = TurnState.PROCESSING; // 플레이어나 적이 사망한 경우에만 실행
    }

    private bool MoveTowardsEnemy(Vector3 target) // 적의 움직임
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime)); // 시작 위치, 타겟 위치, 애니메이션 속도를 변수로 넣어줌
    }

    private bool MoveTowardsStart(Vector3 target) // 적의 움직임
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime)); // 시작 위치, 타겟 위치, 애니메이션 속도를 변수로 넣어줌
    }
}
