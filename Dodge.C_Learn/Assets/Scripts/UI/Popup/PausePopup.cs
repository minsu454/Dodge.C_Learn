using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : BasePopup, ILoadScenePopup
{
    public Text nowScoreText;       //현재 스코어 text

    public SceneType nextScene { get; set; }    //다음 로드 될 씬 들고있는 변수

    protected override void Init()
    {
        base.Init();
        Time.timeScale = 0f;
        nowScoreText.text = GameManager.Instance.score.ToString();
    }
    public override void Close()
    {
        Time.timeScale = 1f;
        base.Close();
    }

    public void LoadSceneAndClose()
    {
        Time.timeScale = 1f;
        Managers.Popup.Clear();
        Managers.Scene.LoadScene(nextScene);
    }
}
