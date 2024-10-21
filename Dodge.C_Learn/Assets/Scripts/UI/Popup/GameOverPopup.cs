using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopup : BasePopup, ILoadScenePopup
{
    public Text ScoreText;

    public SceneType nextScene { get; set; }

    protected override void Init()
    {
        base.Init();
        Time.timeScale = 0f;
        ScoreText.text = GameManager.Instance.score.ToString();
    }
    public void LoadSceneAndClose()
    {
        Time.timeScale = 1f;
        Managers.Popup.Clear();
        Managers.Scene.LoadScene(nextScene);
    }
}
