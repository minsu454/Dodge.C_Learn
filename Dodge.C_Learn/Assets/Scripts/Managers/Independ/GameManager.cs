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
    public PlayerController player;     //플레이어
    public Transform earthTr;           //지구

    public int score;                   //스코어

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

    /// <summary>
    /// 게임 스타트 함수
    /// </summary>
    public void GameStart()
    {
        earthTr.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 3f);
        earthTr.DOMove(new Vector3(0, -7.2f, 0), 3f);
        player.gameObject.transform.DOMove(new Vector3(0, -1.5f, 0), 3f).OnComplete(StageManager.Instance.StartSpawn);
        Managers.Sound.PlaySFX(SfxType.Loading_Player);
    }
    
    /// <summary>
    /// 스코어 더해주는 함수
    /// </summary>
    public void PlusScore(int score)
    {
        this.score += score;
    }

    /// <summary>
    /// 게임오버 함수
    /// </summary>
    public void GameOver()
    {
        Managers.Popup.CreatePopup(PopupType.GameOverPopup);
    }
}
