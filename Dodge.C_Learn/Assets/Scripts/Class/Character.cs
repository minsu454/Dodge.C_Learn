using UnityEngine;

public class Character
{
    private readonly Sprite sprite;
    private readonly RuntimeAnimatorController animator;
    private readonly ScriptableObject info;

    public Sprite Sprite                        { get { return sprite; } }
    public RuntimeAnimatorController Animator   { get { return animator; } }
    public ScriptableObject Info                    { get { return info; } }

    public Character(string path)
    {
        sprite = Resources.Load<Sprite>(path);
        animator = Resources.Load<RuntimeAnimatorController>(path);
        info = Resources.Load<ScriptableObject>(path);
    }
}