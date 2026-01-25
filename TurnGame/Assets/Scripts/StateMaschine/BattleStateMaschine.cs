using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class BattleStateMaschine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT, TAKEACTION, PERFORMACTION
    }
    public PerformAction battleStates;

    public List<HandleTurn> PerformList = new List<HandleTurn>(); 
    public List<GameObject> HerosInBattle = new List<GameObject>(); 
    public List<GameObject> EnemysInBattle = new List<GameObject>(); 

    public enum HeroGUI 
    {
        ACTIVATE, WAITING, INPUT1, INPUT2, DONE
    }

    public HeroGUI HeroInput;

    public List<GameObject> HerosToManage = new List<GameObject>();
    private HandleTurn HeroChoice;

    public GameObject AttackPanel; // 공격 메뉴 패널

    // ★ 삭제된 변수들 (더 이상 필요 없음)
    // public GameObject enemyButton; 
    // public Transform Spacer; 
    // public GameObject EnemySelectPanel; 

    void Start()
    {
        battleStates = PerformAction.WAIT;
        HeroInput = HeroGUI.ACTIVATE;

        AttackPanel.SetActive(false);
        
        // ★ 중요: InputManager에 마우스 이벤트 구독 신청
        if (Managers.Input != null)
        {
            Managers.Input.MouseAction -= OnMouseEvent;
            Managers.Input.MouseAction += OnMouseEvent;
        }
    }

    void Update()
    {
        switch (battleStates)
        {
            case (PerformAction.WAIT):
                if (PerformList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
                break;
            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                if (PerformList[0].Type == "Enemy") 
                {
                    EnemyStateMaschine EM = performer.GetComponent<EnemyStateMaschine>();
                    EM.HeroToAttack = PerformList[0].AttackersTarget;
                    EM.currentstate = EnemyStateMaschine.TurnState.ACTION;
                }

                if (PerformList[0].Type == "Hero") 
                {
                    PlayerStateMaschine HSM = performer.GetComponent<PlayerStateMaschine>();
                    HSM.EnemyToAttack = PerformList[0].AttackersTarget;
                    HSM.currentstate = PlayerStateMaschine.TurnState.ACTION; 
                }
                battleStates = PerformAction.PERFORMACTION;
                break;
            case (PerformAction.PERFORMACTION):
                break;
        }

        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (HerosToManage.Count > 0)
                {
                    // Hero가 활성화되면 Selector와 공격패널을 활성화시킴
                    HerosToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    HeroChoice = new HandleTurn();

                    AttackPanel.SetActive(true);
                    HeroInput = HeroGUI.WAITING; 
                }
                break;
            case (HeroGUI.WAITING):
                break;
            case (HeroGUI.DONE):
                HeroInputDone();
                break;
        }
    }

    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);
    }

    public void InitBattle()
    {
        // ★ 삭제: 이제 버튼을 생성할 필요가 없습니다.
        // EnemyButton(); 
    }

    // ★ 추가됨: 마우스 클릭을 감지하는 함수 (InputManager가 호출해줌)
    void OnMouseEvent(Define.InputEvent evt)
    {
        // 1. 현재 상태가 "적을 선택해야 하는 상태(INPUT2)"가 아니면 무시
        if (HeroInput != HeroGUI.INPUT2) return;

        // 2. 오직 "클릭"했을 때만 반응 (꾹 누르기 등 제외)
        if (evt != Define.InputEvent.Click) return;

        // 3. 레이캐스트 발사 (마우스 위치로 레이저 쏘기)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 레이저가 무언가에 맞았다면
        if (Physics.Raycast(ray, out hit))
        {
            // 4. 맞은 녀석이 "Enemy" 태그를 달고 있는지 확인
            if (hit.collider.CompareTag("Enemy"))
            {
                // 적을 찾았으니 Input2 함수 호출!
                Input2(hit.collider.gameObject);
            }
        }
    }

    public void Input1() // 공격 버튼 클릭 시
    {
        HeroChoice.Attacker = HerosToManage[0].name;
        HeroChoice.AttackersGameObject = HerosToManage[0];
        HeroChoice.Type = "Hero";

        AttackPanel.SetActive(false); 
        
        // ★ 중요: 패널을 켜는 대신, 상태만 '적 선택 대기'로 변경
        // EnemySelectPanel.SetActive(true); (삭제)
        
        HeroInput = HeroGUI.INPUT2; // 이제 마우스 클릭을 기다립니다.
        Debug.Log("공격할 적을 클릭하세요!");
    }

    public void Input2(GameObject choosenEnemy)
    {
        Debug.Log("선택된 적: " + choosenEnemy.name);
        
        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE; 
    }

    void HeroInputDone() 
    {
        PerformList.Add(HeroChoice); 
        
        // EnemySelectPanel.SetActive(false); (삭제)
        
        HerosToManage[0].transform.Find("Selector").gameObject.SetActive(false); 
        HerosToManage.RemoveAt(0); 
        HeroInput = HeroGUI.ACTIVATE; 
    }
}