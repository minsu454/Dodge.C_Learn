using Common.Timer;
using Common.Yield;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public PlayerType playerType;

    private void Start()
    {
        var playerClass = Managers.Character.ReturnAll(playerType);

        Animator anim = player.GetComponentInChildren<Animator>();
        SpriteRenderer spriteRenderer =  player.GetComponentInChildren<SpriteRenderer>();

        anim.runtimeAnimatorController = playerClass.Animator;
        spriteRenderer.sprite = playerClass.Sprite;

        Managers.Popup.CreatePopup(PopupType.InGamePopup);
    }
    
}
