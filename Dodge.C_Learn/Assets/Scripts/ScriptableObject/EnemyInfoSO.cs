using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfoSO", menuName = "SO/EnemyInfoSO", order = 1)]
public class EnemyInfoSO : BaseInfoSO
{
    [Header("Enemy Info")]
    public int Score;           //스코어
    public Sprite[] Sprites;    //적 일반, 피격 스프라이트
}
