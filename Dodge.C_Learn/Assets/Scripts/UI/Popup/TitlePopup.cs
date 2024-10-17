using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePopup : BasePopup
{
    protected override void Init()
    {
        base.Init();
    }
    protected override void Close()
    {
        base.Close();
    }
    public void StartGame()
    {
        Managers.Scene.LoadScene(SceneType.InGame);
    }
}
