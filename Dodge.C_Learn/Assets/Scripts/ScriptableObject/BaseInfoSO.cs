using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "DefaultInfoSO", menuName = "SO/DefaultInfoSO", order = 0)]
public class BaseInfoSO : ScriptableObject
{
    [Header("DefaultInfoSO Info")]
    public int MaxHp;
    public int MaxFireRateCount;
    public float Delay;
    public float ProjectileSpeed;
    public float MoveSpeed;
}
