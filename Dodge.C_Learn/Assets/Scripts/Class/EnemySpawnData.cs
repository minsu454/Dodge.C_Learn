using UnityEngine;

/// <summary>
/// 적 스폰 타입
/// </summary>
[System.Serializable]
public class EnemySpawnData
{
    public EnemyType EnemyType;     //스폰할 적타입
    public Vector3 Pos;             //스폰 위치
}
