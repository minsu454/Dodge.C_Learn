using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePopup : BasePopup
{
    public GameObject pausePanel;
    public Text startTimeText;
    public Text nowTimeText;
    public float startTime;
    public float pauseTime;
    private void Update()
    {
        startTime += Time.deltaTime;
        startTimeText.text = startTime.ToString("0");
    }
    public void Pause()
    {       
        pauseTime = startTime;
        nowTimeText.text = pauseTime.ToString("0");      
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        startTime = pauseTime;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
}
