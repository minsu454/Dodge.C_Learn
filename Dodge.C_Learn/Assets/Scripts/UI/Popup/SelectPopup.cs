using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPopup : BasePopup, ILoadScenePopup
{
    public SceneType nextScene { get; set; }                    //현재 씬
    public List<Image> CharacterList = new List<Image>();       //캐릭터 리스트

    protected override void Init()
    {
        base.Init();
        for (int i = 0; i<CharacterList.Count; i++)
        {
            CharacterList[i].sprite = Managers.Character.ReturnSprite((PlayerType)i);
        }

    }

    /// <summary>
    /// 캐릭터 블루 설정
    /// </summary>
    public void StartBlue()
    {
        Managers.Event.Dispatch(GameEventType.SetPlayerType, PlayerType.BlueJet);
        LoadSceneAndClose();
    }

    /// <summary>
    /// 캐릭터 레드 설정
    /// </summary>
    public void StartRed()
    {
        Managers.Event.Dispatch(GameEventType.SetPlayerType, PlayerType.RedFighter);
        LoadSceneAndClose();
    }

    /// <summary>
    /// 캐릭터 옐로우 설정
    /// </summary>
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