using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "AttackSO", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;
}
