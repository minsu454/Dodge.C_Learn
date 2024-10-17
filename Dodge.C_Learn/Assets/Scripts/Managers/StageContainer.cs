using System;
using System.Collections.Generic;
using UnityEngine;

public class StageContainer : IContainer
{
    private StageSO stageSO;

    public void Init()
    {
        stageSO = Resources.Load<StageSO>($"Stage/StageSO");
    }

    ////public SpawnSO GetSpawn(int stageNum)
    //{
    //    stageNum--;

        List<SpawnSO> spawnList = stageSO.spawnSOList;

        if (0 > stageNum || stageNum >= spawnList.Count)
        {
            throw new IndexOutOfRangeException();
        }

        return spawnList[stageNum];
    }
}

