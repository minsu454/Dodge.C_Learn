using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "DefaultInfoSO", menuName = "SO/DefaultInfoSO", order = 0)]
public class BaseInfoSO : ScriptableObject
{
    [Header("DefaultInfoSO Info")]
    public float FireRate;
    public float delay;
    public float Speed;
}
