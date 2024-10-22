using Common.Timer;
using System;
using UnityEngine;

// 기존 StageContainer의 역할을 stageSO가 하기 때문에,  StageContainer.cs -> UnUse_StageContainer.cs로 한다.
// 또한 StageSO의 정보를 한번에 Spawner에게 넘겨 Spawner가 이를 자체적으로 읽고 스폰하는 방식으로 진행될 것이다.
public class StageManager : SingletonBehaviour<StageManager>
{
    public float patternTime;

    [Header("Spawner")]
    [SerializeField] Spawner spawner;
    [SerializeField] private int curStageIdx = 0;

    private TotalStageDataSO totalStageSO;

    protected override void Awake()
    {
        base.Awake();

        totalStageSO = Resources.Load<TotalStageDataSO>($"StageSO/TotalStageDataSO");
    }

    private void Start()
    {
        Managers.Event.Subscribe(GameEventType.EnemyMoveTimerCompleted, CompletePattern);
    }

    /// <summary>
    /// 스폰 시작해주는 함수
    /// </summary>
    public void StartSpawn()
    {
        RequestEnemySpawn();
        spawner.StartCoSpawnProjectile();
    }

    /// <summary>
    /// 패턴 타이머가 끝나면 다음 패턴 실행시켜주는 함수
    /// </summary>
    public void CompletePattern(object args)
    {
        StartCoroutine(CoTimer.Start(totalStageSO.stageSOList[curStageIdx].NextStageTime, RequestEnemySpawn));
    }

    // Spawner에게 TotalStageSO / StageIdx를 넘긴다.
    private void RequestEnemySpawn()
    {
        Stage curStageSO = totalStageSO.stageSOList[curStageIdx];

        spawner.SpawnStageEnemy(curStageSO);
        
        // 게임이 무한 반복되도록, curStageIdx가 일정 값에 도달했을 때 초기화한다.
        if(curStageIdx >= totalStageSO.stageSOList.Count - 1)
        {
            curStageIdx = 0;
        }  
        else
        {
            // 현재 스테이지의 인덱스를 올려준다.
            curStageIdx++;
        }
    }


    private void OnDestroy()
    {
        Managers.Event.Unsubscribe(GameEventType.EnemyMoveTimerCompleted, CompletePattern);
    }
}