using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CharacterContainer : IContainer
{
    private readonly Dictionary<ObjectType, Dictionary<int, Character>> characterDic = new Dictionary<ObjectType, Dictionary<int, Character>>();

    public void Init()
    {
        characterDic.Add(ObjectType.Player, CreateDic<PlayerType>("Character/Player"));
        characterDic.Add(ObjectType.Enemy, CreateDic<EnemyType>("Character/Enemy"));
    }

    private Dictionary<int, Character> CreateDic<T>(string path) where T : Enum
    { 
        Dictionary<int, Character> tempDic = new Dictionary<int, Character>();

        foreach (T type in Enum.GetValues(typeof(T)))
        {
            string sType = type.ToString();
            string name = $"{path}/{sType}/{sType}";
            Character character = new Character(name);

            tempDic.Add((int)(object)type, character);
        }

        return tempDic;
    }

    public Character ReturnAll(PlayerType type)
    {
        if (!characterDic[ObjectType.Player].TryGetValue((int)type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result;
    }

    public Sprite ReturnSprite(PlayerType type)
    {
        if (!characterDic[ObjectType.Player].TryGetValue((int)type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }
        
        return result.Sprite;
    }

    public RuntimeAnimatorController ReturnAnimator(PlayerType type)
    {
        if (!characterDic[ObjectType.Player].TryGetValue((int)type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.Animator;
    }

    public AttackSO ReturnAttackSO(PlayerType type)
    {
        if (!characterDic[ObjectType.Player].TryGetValue((int)type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.AttackSO;
    }

    public Character ReturnAll(EnemyType type)
    {
        if (!characterDic[ObjectType.Enemy].TryGetValue((int)type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result;
    }

    public Sprite ReturnSprite(EnemyType type)
    {
        if (!characterDic[ObjectType.Enemy].TryGetValue((int)type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.Sprite;
    }

    public RuntimeAnimatorController ReturnAnimator(EnemyType type)
    {
        if (!characterDic[ObjectType.Enemy].TryGetValue((int)type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.Animator;
    }

    public AttackSO ReturnAttackSO(EnemyType type)
    {
        if (!characterDic[ObjectType.Enemy].TryGetValue((int)type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.AttackSO;
    }
}
