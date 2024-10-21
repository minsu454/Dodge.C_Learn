using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : BasePopup, ILoadScenePopup
{
    public Text nowTimeText;

    public SceneType nextScene { get; set; }

    protected override void Init()
    {
        base.Init();
        Time.timeScale = 0f;
    }
    public void SetNowtime(float time)
    {
        nowTimeText.text = time.ToString("0");
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
