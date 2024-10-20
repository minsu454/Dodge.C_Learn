using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TotalStageDataSO", menuName = "SO/TotalStageDataSO", order = 1)]
public class TotalStageDataSO : ScriptableObject
{
    public List<StageSO> stageSOList = new List<StageSO>();
}

[Serializable]
public class StageSO
{
    public int spawnTime;
    public PatternSO PatternList;
}