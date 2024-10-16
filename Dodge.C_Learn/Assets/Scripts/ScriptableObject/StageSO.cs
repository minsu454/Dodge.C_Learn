using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageSO", menuName = "SO/StageSO", order = 1)]
public class StageSO : ScriptableObject
{
    public List<SpawnSO> spawnSOList = new List<SpawnSO>();
}
