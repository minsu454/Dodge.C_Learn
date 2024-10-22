using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePopup : BasePopup
{ 
    /// <summary>
    /// SelectPopup 생성 해주는 함수
    /// </summary>
    public void LoadSelectPopup()
    {
        BasePopup basePopup = Managers.Popup.CreatePopup(PopupType.SelectPopup);
        SelectPopup selectPopup = basePopup as SelectPopup;
        selectPopup.nextScene = SceneType.InGame;
    }
}
