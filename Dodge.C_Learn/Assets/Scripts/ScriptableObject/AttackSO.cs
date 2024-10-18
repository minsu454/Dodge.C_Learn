using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "AttackSO", menuName = "SO/AttackSO", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float FireRate;
    public float delay;
    public float Speed;

    [Header("Enemy Info")]
    public int bulletAmount;
    public int fireCount;

    [Header("Player Inof")]
    public string ProjectileA;
    public string ProjectileB;
}