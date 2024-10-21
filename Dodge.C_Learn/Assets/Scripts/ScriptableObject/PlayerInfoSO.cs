using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoSO", menuName = "SO/PlayerInfoSO", order = 2)]
public class PlayerInfoSO : BaseInfoSO
{
    [Header("Player Info")]
    public string ProjectileA;
    public string ProjectileB;
}
