using System;
using System.Collections.Generic;

[Serializable]
public class Pattern
{
    public string name;
    public List<EnemySpawnData> spawnPointList = new List<EnemySpawnData>();
}