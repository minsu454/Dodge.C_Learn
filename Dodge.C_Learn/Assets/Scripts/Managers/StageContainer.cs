using System;
using System.Collections.Generic;
using UnityEngine;

public class StageContainer : IContainer
{
    private TotalStageDataSO stageSO;

    public void Init()
    {
        stageSO = Resources.Load<TotalStageDataSO>($"Stage/StageSO");
    }

    public Patten GetSpawn(int stageNum)
    {
        stageNum--;

        List<Patten> spawnList = stageSO.spawnSOList;

       if (0 > stageNum || stageNum >= spawnList.Count)
       {
           throw new IndexOutOfRangeException();
       }

       return spawnList[stageNum];
    }
}

