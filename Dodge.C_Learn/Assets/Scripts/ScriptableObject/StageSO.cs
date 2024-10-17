using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageSO", menuName = "SO/StageSO", order = 1)]
public class StageSO : ScriptableObject
{
    [Header("Stage Info")]
    public List<SpawnSO> spawnSOList = new List<SpawnSO>();
}
