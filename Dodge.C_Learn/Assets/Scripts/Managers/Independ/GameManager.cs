using Common.Timer;
using Common.Yield;
using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{
    public PlayerController player;
    public PlayerType playerType;

    public int score;

    protected override void Awake()
    {
        base.Awake();
        player.SetPlayer(playerType);
    }

    private void Start()
    {
        Managers.Popup.CreatePopup(PopupType.InGamePopup);
        player.gameObject.transform.DOMove(new Vector3(0, -3, 0), 1.5f).OnComplete(StageManager.Instance.StartSpawn);
    }
    
    public void PlusScore(int score)
    {
        this.score += score;
    }
}
