using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMaschine : MonoBehaviour
{
    private BattleStateMaschine BSM;

    public enum TurnState
    {
        PROCESSING, // 턴 처리 상태
        ADDTOLIST, // 플레이어를 리스트에 추가함
        WAITING, // 대기 중 상태
        SELECTING,
        ACTION, // 행동
        DEAD // 죽음
    }

    public TurnState currentstate;

    // 진행률 표시 줄에 사용
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image ProgressBar; // 진행률 표시줄 가져옴
    public GameObject Selector;
    // IeNumerator
    public GameObject EnemyToAttack; // Enemy근처에서 공격할 수 있도록 하는 변수
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 50f;

    void Start()
    {

        startPosition = transform.position;
        cur_cooldown = Random.Range(0, 2.5f); // 각 아군마다 대기시간 다르게 설정 (운, 속도 등 다른 선 턴 개념으로 사용해도 됨)
        Selector.gameObject.SetActive(false);
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMaschine>();
        currentstate = TurnState.PROCESSING; // 게임 시작하고 바로 턴 처리 상태가 활성화
    }

    void Update()
    {
        // Debug.Log(currentstate);
        switch (currentstate)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;
            case (TurnState.ADDTOLIST):
                // 쿨다운이 끝나면 자동으로 리스트에 추가
                BSM.HerosToManage.Add(this.gameObject);
                currentstate = TurnState.WAITING; // 대기상태로 전환
                break;
            case (TurnState.WAITING):
                // idle
                break;
            case (TurnState.ACTION):
                Vector3 localOffset = new Vector3(-2.0f, 1.0f, -1f);
                Vector3 targetPosition = this.transform.TransformPoint(localOffset);
                Quaternion addRotation = Quaternion.Euler(20, 75, 0);
                Quaternion targetRotation = this.transform.rotation * addRotation;
                Camera.main.transform.SetPositionAndRotation(targetPosition, targetRotation);
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):

                break;
        }
       
    }

    public void Setup(Image myBar)
    {
        this.ProgressBar = myBar; // 스포너가 준 바를 내 것으로 등록

        // 초기화
        cur_cooldown = 0;
        currentstate = TurnState.PROCESSING;

        if (ProgressBar != null) ProgressBar.fillAmount = 0; // 0으로 시작!
    }

    void UpgradeProgressBar() // 진행률 표시줄 업그레이드
    {
        // 안전장치: 바가 연결 안 됐으면 실행 중지
        if (ProgressBar == null) return;

        cur_cooldown = cur_cooldown + Time.deltaTime; // 쿨다운 시간이 증가
        float calc_cooldown = cur_cooldown / max_cooldown; // 쿨다운의 양 계산

        // 계산된 쿨다운 값의 x값을 통해 프로그레스바 증가시킴
        ProgressBar.transform.localScale = new Vector3(
            Mathf.Clamp(calc_cooldown, 0, 1),
            ProgressBar.transform.localScale.y,
            ProgressBar.transform.localScale.z
        );

        if (cur_cooldown >= max_cooldown) // 쿨다운이 최대치인 5초를 초과하면 상태를 변경
        {
            currentstate = TurnState.ADDTOLIST;
        }

    }

    private IEnumerator TimeForAction() // 시간 제한 행동 (행동을 시간 순서대로 수행하도록 함)
    {
        if (actionStarted) // 동작이 이미 실행됐다면 빠져나옴
        {
            yield break;
        }
        actionStarted = true;
        // 플레이어 근처에서 공격하도록 위치 설정
        Vector3 enemyPosition = new Vector3(EnemyToAttack.transform.position.x - 1.5f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z); // 플레이어 위치보다 살짝 앞으로 설정




        while (MoveTowardsEnemy(enemyPosition))
        {
            yield return null; // 적을 향해 이동하는 동안 아무것도 하지 않고 기다림.
        }

        // 잠시 기다렸다가
        yield return new WaitForSeconds(0.5f);
        // 피해를 주고 

        // 다시 초기 위치로 돌아감
        Vector3 firstPosition = startPosition;
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
