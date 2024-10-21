using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoSO", menuName = "SO/PlayerInfoSO", order = 2)]
public class PlayerInfoSO : BaseInfoSO
{
    [Header("Player Info")]
    public string ProjectileA;      //약한 투사체
    public string ProjectileB;      //강한 투사체
}
