using System;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    private StageContainer stageContainer;

    // 현재 StageCase / StageSO / SpawnSO 이렇게 나뉘어져 있다.
    // 추 후 한개로 통합을 하자.
    [Header("Prefabs")]
    [SerializeField] StageCase[] stagePrefabs;

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
        for(int i = 0; i < stagePrefabs.Length; i++)
        {
            StageCase stageCase = stagePrefabs[i];
            InitiallizeStage(stageCase);
        }


    }

    // 변수로 어느것을 넘겨야하나...
    public void InitiallizeStage(StageCase stageCase)
    {
        stageContainer.InitiallizeStage(stageCase);
    }

    // 변수로 어느것을 넘겨야하나...
    public void GetStage()
    {

    }
}