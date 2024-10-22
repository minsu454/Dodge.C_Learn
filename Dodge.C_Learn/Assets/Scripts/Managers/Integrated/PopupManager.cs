using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PopupManager : MonoBehaviour, ICreate
{
    private readonly Dictionary<PopupType, GameObject> popupContainerDic = new Dictionary<PopupType, GameObject>();     //팝업 타입 별로 프리팹 저장하는 dic
    private readonly Stack<GameObject> depth = new Stack<GameObject>();                                                 //팝업 뎁스

    private Transform mainCanvas;   //Scene마다 있는 mainCanvas저장 변수

    public void Init()
    {
        foreach (PopupType type in Enum.GetValues(typeof(PopupType)))
        {
            GameObject go = Resources.Load<GameObject>(string.Format($"Prefabs/Popup/{type.ToString()}"));
            popupContainerDic.Add(type, go);
        }

        Managers.Scene.OnLoadCompleted(FindMainCanvas);
        Managers.Scene.OnLoadCompleted(ResetPopup);
    }

    /// <summary>
    /// MainCanvas 찾아주는 함수
    /// </summary>
    public void FindMainCanvas(Scene scene, LoadSceneMode mode)
    {
        GameObject mainCanvasObj = GameObject.Find("MainCanvas");
        if (mainCanvasObj == null)
        {
            Debug.LogError("Is Not MainCanvas.");
            return;
        }

        mainCanvas = mainCanvasObj.transform;
    }

    /// <summary>
    /// 팝업 전부 지우는 함수
    /// </summary>
    public void ResetPopup(Scene scene, LoadSceneMode mode)
    {
        while (depth.Count != 0)
        {
            Close();
        }
    }
    public void Clear()
    {
        depth.Clear();
    }

    /// <summary>
    /// 팝업 생성 함수
    /// </summary>
    public BasePopup CreatePopup(PopupType type, bool useMainCanvas = true, bool curPopupActive = true)
    {
        if (!popupContainerDic.TryGetValue(type, out GameObject popupGo))
        {
            Debug.LogError("Is Not BasePopup : popupContainerDic");
            return null;
        }

        GameObject clone = Instantiate(popupGo, useMainCanvas ? mainCanvas : null);

        if (depth.TryPeek(out GameObject go) && curPopupActive)
        {
            go.SetActive(false);
        }

        depth.Push(clone);

        BasePopup popup = clone.GetComponent<BasePopup>();

        return popup;
    }

    /// <summary>
    /// 팝업 닫는 함수
    /// </summary>
    public void Close()
    {
        if (depth.Count == 0)
        {
            return;
        }

        Destroy(depth.Pop());

        if (depth.TryPeek(out GameObject go))
        {
            go.SetActive(true);
        }
    }

    /// <summary>
    /// 지금 depth에 있는 오브젝트가 지금 오브젝트와 같은지 비교해주는 함수
    /// </summary>
    public bool ComparerLastDepth(GameObject go)
    {
        depth.TryPeek(out GameObject temp);

        if (temp == null || temp != go)
        {
            return false;
        }
        return true;
    }
}
