using UnityEngine;
using UnityEngine.UI;

public class TestSpawner : MonoBehaviour
{
    // [인스펙터에서 연결할 UI 변수들]
    public Image uiBar_Player1;
    public Image uiBar_Player2;

    void Start()
    {
        Managers.Data.Init();

        // ★ [추가 1] 매니저(BattleStateMaschine)를 먼저 찾아옵니다.
        // (주의: 씬에 "BattleManager"라는 이름의 오브젝트가 있어야 합니다)
        BattleStateMaschine BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMaschine>();

        // 2. 스폰 요청
        GameObject player1 = Managers.Game.Spawn(Define.WorldObject.Player, "MyPlayer");
        GameObject player2 = Managers.Game.Spawn(Define.WorldObject.Player, "MyPlayer");
        GameObject enemy1 = Managers.Game.Spawn(Define.WorldObject.Player, "Enemy");
        GameObject enemy2 = Managers.Game.Spawn(Define.WorldObject.Player, "Enemy");

        // 3. 위치 잡고 + UI 쥐여주기 + ★ 매니저에 등록하기
        if (player1 != null)
        {
            player1.transform.position = new Vector3(-4, 1.5f, -1.5f);
            player1.name = "Player1";

            // UI 연결
            PlayerStateMaschine psm = player1.GetComponent<PlayerStateMaschine>();
            if (psm != null) psm.Setup(uiBar_Player1);

            // ★ [추가 2] 매니저의 영웅 명단에 등록
            BSM.HerosInBattle.Add(player1);
        }

        if (player2 != null)
        {
            player2.transform.position = new Vector3(-4, 1.5f, 3);
            player2.name = "Player2";

            // UI 연결
            PlayerStateMaschine psm = player2.GetComponent<PlayerStateMaschine>();
            if (psm != null) psm.Setup(uiBar_Player2);

            // ★ [추가 3] 매니저의 영웅 명단에 등록
            BSM.HerosInBattle.Add(player2);
        }

        if (enemy1 != null)
        {
            enemy1.transform.position = new Vector3(4, 1.5f, -1.5f);
            enemy1.name = "Enemy1";

            // ★ [추가 4] 매니저의 적 명단에 등록
            BSM.EnemysInBattle.Add(enemy1);
        }

        if (enemy2 != null)
        {
            enemy2.transform.position = new Vector3(4, 1.5f, 3);
            enemy2.name = "Enemy2";

            // ★ [추가 5] 매니저의 적 명단에 등록
            BSM.EnemysInBattle.Add(enemy2);
        }

        // ★ [추가 6] 모든 소환과 등록이 끝났으니, 전투 초기화(버튼 생성 등)를 시작하라고 명령
        BSM.InitBattle();
    }
}