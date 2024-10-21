using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TotalStageDataSO", menuName = "SO/TotalStageDataSO", order = 11)]
public class TotalStageDataSO : ScriptableObject
{
    public List<StageSO> stageSOList = new List<StageSO>();
}

[Serializable]
public class StageSO
{
    public float NextStageTime;
    public float DurationTime;
    public PatternSO PatternList;
}