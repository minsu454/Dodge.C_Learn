using UnityEngine;

public class Character
{
    private readonly Sprite sprite;
    private readonly RuntimeAnimatorController animator;
    private readonly AttackSO attackSO;

    public Sprite Sprite                        { get { return sprite; } }
    public RuntimeAnimatorController Animator   { get { return animator; } }
    public AttackSO AttackSO                    { get { return attackSO; } }

    public Character(string path)
    {
        sprite = Resources.Load<Sprite>(path);
        animator = Resources.Load<RuntimeAnimatorController>(path);
        attackSO = Resources.Load<AttackSO>(path);
    }
}