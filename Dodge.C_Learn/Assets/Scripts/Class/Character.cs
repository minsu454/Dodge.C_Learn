using UnityEngine;

/// <summary>
/// 캐릭터 정보
/// </summary>
public class Character
{
    private readonly Sprite sprite;                         //기본 스프라이트
    private readonly RuntimeAnimatorController animator;    //캐릭터 애니메이터
    private readonly ScriptableObject info;                 //스텟 정보

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