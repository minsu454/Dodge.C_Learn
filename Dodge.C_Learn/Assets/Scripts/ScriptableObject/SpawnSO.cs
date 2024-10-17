using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SpawnSO", menuName = "SO/SpawnSO", order = 2)]
public class SpawnSO : ScriptableObject
{
    [Header("Spawn Info")]
    public int count;
    public int spawnTime;
}