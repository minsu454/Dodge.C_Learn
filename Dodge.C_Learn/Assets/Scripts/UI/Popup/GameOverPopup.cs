using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임오버 팝업
/// </summary>
public class GameOverPopup : BasePopup, ILoadScenePopup
{
    public Text ScoreText;      //스코어text

    public SceneType nextScene { get; set; }    //다음 씬 저장 변수

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
