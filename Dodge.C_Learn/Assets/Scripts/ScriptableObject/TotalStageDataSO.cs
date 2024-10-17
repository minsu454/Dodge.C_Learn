using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TotalStageDataSO", menuName = "SO/TotalStageDataSO", order = 1)]
public class TotalStageDataSO : ScriptableObject
{
    public int spawnTime;
    public List<Patten> spawnSOList = new List<Patten>();
}
