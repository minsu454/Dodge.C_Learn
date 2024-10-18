using Common.Timer;
using Common.Yield;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public PlayerType playerType;

    private void Awake()
    {
        player.SetPlayer(playerType);
    }

    private void Start()
    {
        Managers.Popup.CreatePopup(PopupType.InGamePopup);
    }
    
}
