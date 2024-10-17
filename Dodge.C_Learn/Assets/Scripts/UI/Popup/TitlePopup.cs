using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePopup : BasePopup
{
    public override void Init()
    {
        base.Init();
    }
    public override void Close()
    {
        base.Close();
    }
    public void StartGame()
    {
        Managers.Scene.LoadScene(SceneType.InGame);
    }
}
