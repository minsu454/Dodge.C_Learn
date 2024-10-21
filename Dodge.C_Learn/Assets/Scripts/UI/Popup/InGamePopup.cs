using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePopup : BasePopup
{
    public Text score;
    public Text startTimeText;  
    public float startTime;
    private void Update()
    {
        startTime += Time.deltaTime;
        startTimeText.text = startTime.ToString("0");
        score.text = GameManager.Instance.score.ToString();
    }
    public void Pause()
    {       
        Managers.Popup.CreatePopup(PopupType.PausePopup);
    }
}
