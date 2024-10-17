using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : BasePopup
{
    public Text nowTimeText;
    public override void Init()
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
    public void Retry()
    {
        Managers.Scene.LoadScene(SceneType.Title);
    }
}
