using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public sealed class CharacterContainer : IContainer
{
    private readonly Dictionary<Enum, Character> characterDic = new Dictionary<Enum, Character>();  //character데이터 담아주는 dictionary

    public void Init()
    {
        CreateDic<PlayerType>("Character/Player");
        CreateDic<EnemyType>("Character/Enemy");
    }

    /// <summary>
    /// dictionary enum 값으로 만들어주는 함수
    /// </summary>
    private void CreateDic<T>(string path) where T : Enum
    { 
        foreach (T type in Enum.GetValues(typeof(T)))
        {
            string sType = type.ToString();
            string name = $"{path}/{sType}/{sType}";
            Character character = new Character(name);

            characterDic.Add(type, character);
        }
    }

    /// <summary>
    /// Character class 리턴해주는 함수
    /// </summary>
    public Character ReturnAll(Enum type)
    {
        if (!characterDic.TryGetValue(type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result;
    }

    /// <summary>
    /// sprite 리턴해주는 함수
    /// </summary>
    public Sprite ReturnSprite(Enum type)
    {
        if (!characterDic.TryGetValue(type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.Sprite;
    }

    /// <summary>
    /// animator 리턴해주는 함수
    /// </summary>
    public RuntimeAnimatorController ReturnAnimator(Enum type)
    {
        if (!characterDic.TryGetValue(type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.Animator;
    }

    /// <summary>
    /// AttackSO 리턴해주는 함수
    /// </summary>
    public ScriptableObject ReturnAttackSO(Enum type)
    {
        if (!characterDic.TryGetValue(type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.Info;
    }
}
