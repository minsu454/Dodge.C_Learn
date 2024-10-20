using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfoSO", menuName = "SO/EnemyInfoSO", order = 1)]
public class EnemyInfoSO : BaseInfoSO
{
    [Header("Enemy Info")]
    public int BulletAmount;
    public int FireCount;
    public int Score;
    public Sprite[] Sprites;
}
