using System;
using UnityEngine;

// 기존 StageContainer의 역할을 stageSO가 하기 때문에,  StageContainer.cs -> UnUse_StageContainer.cs로 한다.
// 또한 StageSO의 정보를 한번에 Spawner에게 넘겨 Spawner가 이를 자체적으로 읽고 스폰하는 방식으로 진행될 것이다.
public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public float patternTime;

    private int curStageIdx = 0;

    [Header("Spawner")]
    [SerializeField] Spawner spawner;

    private TotalStageDataSO totalStageSO;

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

        totalStageSO = Resources.Load<TotalStageDataSO>($"Stage/TotalStageDataSO");
    }

    private void Start()
    {
        RequestEnemySpawn();
    }

    // Spawner에게 TotalStageSO / StageIdx를 넘긴다.
    public void RequestEnemySpawn()
    {
        StageSO curStageSO = totalStageSO.stageSOList[curStageIdx];

        PatternSO curPatternSO = curStageSO.PatternList;

        Pattern pattern = curStageSO.PatternList.pattern;

        // 현재 스테이지의 인덱스를 올려준다.
        curStageIdx++;

        spawner.SpawnStageEnemy(curPatternSO);
    }




}