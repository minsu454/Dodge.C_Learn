using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "AttackSO", menuName = "SO/AttackSO", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public GameObject ProjectileA;
    public GameObject ProjectileB;
    public float FireRate;
    public float Speed;

}
