using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class StageContainer : MonoBehaviour, IContainer
{
    private StageSO stageSO;

    public void Init()
    {
        stageSO = Resources.Load<StageSO>($"Stage/StageSO");
    }

    public SpawnSO GetSpawn(int stageNum)
    {
        stageNum--;

        List<SpawnSO> spawnList = stageSO.spawnSOList;

        if (0 > stageNum || stageNum >= spawnList.Count)
        {
            throw new IndexOutOfRangeException();
        }

        return spawnList[stageNum];
    }

    // 실질적으로 Json 파일을 읽는 과정.
    public void InitiallizeStage(StageCase stageCase)
    {
        // 추 후 인덱스별로 json 파일명을 구분해서 접근하는 것이 좋아보인다.
        string jsonName = $"/{"ㅁㄴㅇ"}.json";

        string fileName = Path.Combine(Application.persistentDataPath + jsonName);

        if(System.IO.File.Exists(fileName))
        {
            stageCase = new StageCase();
            string jsonOrigin = JsonUtility.ToJson(stageCase);
            stageCase = JsonUtility.FromJson<StageCase>(jsonOrigin);
        }
    }


}