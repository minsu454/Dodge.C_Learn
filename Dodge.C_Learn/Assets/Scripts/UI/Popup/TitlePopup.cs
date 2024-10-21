using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePopup : BasePopup
{ 
    public void LoadSelectPopup()
    {
        BasePopup basePopup = Managers.Popup.CreatePopup(PopupType.SelectPopup);
        SelectPopup selectPopup = basePopup as SelectPopup;
        selectPopup.nextScene = SceneType.InGame;
    }
}
