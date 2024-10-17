using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePopup : BasePopup
{
    public Text startTimeText;  
    public float startTime;
    private void Update()
    {
        startTime += Time.deltaTime;
        startTimeText.text = startTime.ToString("0");
    }
    public void Pause()
    {       
        BasePopup basePopup = Managers.Popup.CreatePopup(PopupType.PausePopup);
        PausePopup pausePopup = basePopup as PausePopup;
        pausePopup.SetNowtime(startTime);
    }
}
