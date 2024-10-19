using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfoSO", menuName = "SO/EnemyInfoSO", order = 1)]
public class EnemyInfoSO : BaseInfoSO
{
    [Header("Enemy Info")]
    public int bulletAmount;
    public int fireCount;
    public Sprite[] sprites;
}
