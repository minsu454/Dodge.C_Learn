using System;
using System.Collections.Generic;

[Serializable]
public class Pattern
{
    public string name;                                                         //패턴 이름
    public List<EnemySpawnData> spawnPointList = new List<EnemySpawnData>();    //패턴에 나오는 EnemySpawnData List
}