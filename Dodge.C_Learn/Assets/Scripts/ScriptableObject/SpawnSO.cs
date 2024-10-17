using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SpawnSO", menuName = "SO/SpawnSO", order = 2)]
public class SpawnSO : ScriptableObject
{
    public int count;
    public int spawnTime;
}