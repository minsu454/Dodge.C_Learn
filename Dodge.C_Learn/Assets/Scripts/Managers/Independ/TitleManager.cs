using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {      
        Managers.Popup.CreatePopup(PopupType.TitlePopup);
    }
}
