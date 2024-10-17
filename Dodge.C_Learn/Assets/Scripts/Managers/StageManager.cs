using System;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    private StageContainer stageContainer;
    public float patternTime { get; private set; } = 30;
    private float curTimer { get; set; } = 0;

    // 현재 StageCase / StageSO / SpawnSO 이렇게 나뉘어져 있다.
    // 추 후 한개로 통합을 하자.
    [Header("Prefabs")]
    [SerializeField] StageCase[] stagePrefabs;

    [Header("Spawner")]
    [SerializeField] Spawner spawner;

    public static StageManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        stageContainer = gameObject.AddComponent<StageContainer>();

        StageSetting();
    }

    private void StageSetting()
    {
        // 이 과정은 쓸모 없을 것임.
/*      for(int i = 0; i < stagePrefabs.Length; i++)
        {
            StageCase stageCase = stagePrefabs[i];
            InitiallizeStage(stageCase);
        }
*/

        // 초기 StageSO에 List 형식으로 각 Stage에 관한 내용이 담겨 있다.
        // 즉, 시작하자마자 초기화를 하는 셈이다.
        // 기존 StageContainer에 작성한 Json Reading은 필요가 없을 것 같다.


        // SpawnManager / StageManager의 내용이 분리가 되어 있으므로
        // StageSO에 작성된 내용을 바탕으로 아래 기능을 수행한다.
        // 0. Stage 몬스터 Spawn -> SpawnManager에게 인자 넘기기.
        // 1. Stage 몬스터 등장 패턴 순서
        // 2. Stage 패턴 시간 / End 조건 정의 
        // 3. Stage boss 등장 / Item 등장 등의 이벤트 발생 유무 
        // -> EventManager를 사용해 Spawn(보스, 아이템) 등의 event를 추가해도 될지도?
        // 아니면 자체적으로 StageManager 내부에서 처리하는 방식도 가능할 지도 모르겠다.

        // 문제점 
        // Q. 사용자도 공격을 할 수있음에도 불구하고, Stage의 패턴을 끝내는
        // 조건을 무조건 시간초로 할 것인가?
        
        // X(현재는 고려하지 않는다.) -> A1. 몰살 : 스폰된 적의 수를 기억한 다음, 이를 감지하는 방식으로 동작한다.
        // X(현재는 고려하지 않는다.) -> A2. 특정 보스 : 특정 몬스터(대장격)이 죽으면 이벤트를 호출한다.
       
        // 30초 전에는 일정 초 주기로 몬스터가 스폰되게 한다.
        // 30초가 지나면 특정 몬스터가 배열 / 커다란 하나(보스)로 스폰되어서
        // 해당 패턴을 클리어 할 때까지 계속 된다...
    }

    // 시간, SpawnSO -> 이름 바뀔 꺼임(StageSO), 

    // Spawner에게 Stage에서 스폰할 몬스터 정보 전달.
    // 몬스터 정보, 수, 스폰 좌표 등이 인자로 들어가지 않을까 싶다.
    public void RequestEnemySpawn()
    {

    }

    // 이 과정은 쓸모 없을 것임.
    public void InitiallizeStage(StageCase stageCase)
    {
        stageContainer.InitiallizeStage(stageCase);
    }
}