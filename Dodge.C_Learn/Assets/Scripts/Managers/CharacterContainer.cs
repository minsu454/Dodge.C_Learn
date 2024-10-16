using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CharacterContainer : IContainer
{
    private readonly Dictionary<PlayerType, Character> playerClassDic = new Dictionary<PlayerType, Character>();

    public void Init()
    {
        foreach (PlayerType type in Enum.GetValues(typeof(PlayerType)))
        {
            string sType = type.ToString();
            string name = $"Character/{sType}/{sType}";
            Character character = new Character(name);

            playerClassDic.Add(type, character);
        }
    }

    public Character ReturnAll(PlayerType type)
    {
        if (!playerClassDic.TryGetValue(type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result;
    }

    public Sprite ReturnSprite(PlayerType type)
    {
        if (!playerClassDic.TryGetValue(type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.Sprite;
    }

    public RuntimeAnimatorController ReturnAnimator(PlayerType type)
    {
        if (!playerClassDic.TryGetValue(type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.Animator;
    }

    public AttackSO ReturnAttackSO(PlayerType type)
    {
        if (!playerClassDic.TryGetValue(type, out Character result))
        {
            Debug.Log($"Is Not Find PlayerClassDic {type}");
            return null;
        }

        return result.AttackSO;
    }
}
