using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PattenSO", menuName = "SO/PattenSO", order = 2)]
public class PattenSO : ScriptableObject
{
    public List<EnemySpawnData> spawnPointList = new List<EnemySpawnData>();
}