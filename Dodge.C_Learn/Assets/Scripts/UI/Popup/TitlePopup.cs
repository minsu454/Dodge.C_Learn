using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePopup : BasePopup, ILoadScenePopup
{
    public SceneType nextScene {  get; set; }

    public void LoadSceneAndClose()
    {
        Managers.Popup.Clear();
        Managers.Scene.LoadScene(nextScene);
    }
}
