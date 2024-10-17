using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    private void Start()
    {      
        Managers.Popup.CreatePopup(PopupType.PlayButtonPopup);
    }
}
