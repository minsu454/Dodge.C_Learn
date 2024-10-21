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
    public Transform earthTr;

    public PlayerType playerType;

    public int score;

    protected override void Awake()
    {
        base.Awake();
        player.SetPlayer(Managers.Data.PlayerType);
    }

    private void Start()
    {
        Managers.Popup.CreatePopup(PopupType.InGamePopup);

        GameStart();
    }

    public void GameStart()
    {
        earthTr.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 3f);
        earthTr.DOMove(new Vector3(0, -7.2f, 0), 3f);
        player.gameObject.transform.DOMove(new Vector3(0, -1.5f, 0), 3f).OnComplete(StageManager.Instance.StartSpawn);
        Managers.Sound.PlaySFX(SfxType.Loading_Player);
    }
    
    public void PlusScore(int score)
    {
        this.score += score;
    }

    public void GameOverPopup()
    {
        Managers.Popup.CreatePopup(PopupType.GameOverPopup);
    }
}
