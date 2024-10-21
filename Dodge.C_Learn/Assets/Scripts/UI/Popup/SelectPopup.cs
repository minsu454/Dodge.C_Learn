using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPopup : BasePopup, ILoadScenePopup
{
    public SceneType nextScene { get; set; }
    public List<Image> children = new List<Image>();
    protected override void Init()
    {
        base.Init();
        for (int i = 0; i<children.Count; i++)
        {
            children[i].sprite = Managers.Character.ReturnSprite((PlayerType)i);
        }

    }

    public void StartBlue()
    {
        Managers.Event.Dispatch(GameEventType.SetPlayerType, PlayerType.BlueJet);
        LoadSceneAndClose();
    }
    public void StartRed()
    {
        Managers.Event.Dispatch(GameEventType.SetPlayerType, PlayerType.RedFighter);
        LoadSceneAndClose();
    }
    public void StartYellow()
    {
        Managers.Event.Dispatch(GameEventType.SetPlayerType, PlayerType.YellowCanaria);
        LoadSceneAndClose();
    }

    public void LoadSceneAndClose()
    {
        Managers.Popup.Clear();
        Managers.Scene.LoadScene(nextScene);
    }
}