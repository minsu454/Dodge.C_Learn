using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {      
        BasePopup basePopup = Managers.Popup.CreatePopup(PopupType.TitlePopup);
        TitlePopup titlePopup = basePopup as TitlePopup;
        titlePopup.nextScene = SceneType.InGame;
    }
}
