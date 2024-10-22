using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "DefaultInfoSO", menuName = "SO/DefaultInfoSO", order = 0)]
public class BaseInfoSO : ScriptableObject
{
    [Header("DefaultInfoSO Info")]
    public int MaxHp;                   //최대 체력
    public int MaxFireRateCount;        //연사력 갯수
    public float Delay;                 //발사 딜레이
    public float ProjectileSpeed;       //투사체 속도
    public float MoveSpeed;             //움직이는 속소
}
