using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TotalStageDataSO", menuName = "SO/TotalStageDataSO", order = 11)]
public class TotalStageDataSO : ScriptableObject
{
    public List<Stage> stageSOList = new List<Stage>();     //스테이지 총괄 리스트
}
