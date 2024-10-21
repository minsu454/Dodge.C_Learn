using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfoSO", menuName = "SO/EnemyInfoSO", order = 1)]
public class EnemyInfoSO : BaseInfoSO
{
    [Header("Enemy Info")]
    public int Score;
    public Sprite[] Sprites;
}
