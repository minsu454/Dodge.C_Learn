using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class StageContainer : MonoBehaviour, IContainer
{
    private TotalStageDataSO stageSO;

    public void Init()
    {
        stageSO = Resources.Load<TotalStageDataSO>($"Stage/StageSO");
    }

    //public Pattern GetSpawn(int stageNum)
    //{
    //    stageNum--;

    //    List<Pattern> patternList = stageSO.PatternList;

    //    if (0 > stageNum || stageNum >= patternList.Count)
    //    {
    //        throw new IndexOutOfRangeException();
    //    }

    //    return patternList[stageNum];
    //}

    // 실질적으로 Json 파일을 읽는 과정.
    public void InitiallizeStage(StageCase stageCase)
    {
        // 추 후 인덱스별로 json 파일명을 구분해서 접근하는 것이 좋아보인다.
        string jsonName = $"/{"ㅁㄴㅇ"}.json";

        string fileName = Path.Combine(Application.persistentDataPath + jsonName);

        if (System.IO.File.Exists(fileName))
        {
            stageCase = new StageCase();
            string jsonOrigin = JsonUtility.ToJson(stageCase);
            stageCase = JsonUtility.FromJson<StageCase>(jsonOrigin);
        }
    }
}

   


