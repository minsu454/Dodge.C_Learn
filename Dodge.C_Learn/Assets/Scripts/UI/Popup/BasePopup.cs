using System;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    public Action ClossEvent;       // 팝업 닫힐때 호출되는 함수

    private void OnEnable()
    {
        Init();
    }

    /// <summary>
    /// 생성 함수
    /// </summary>
    public virtual void Init()
    {
        
    }

    /// <summary>
    /// 닫기 함수
    /// </summary>
    public virtual void Close()
    {
        if (!Managers.Popup.ComparerLastDepth(gameObject))
            return;
        
        ClossEvent?.Invoke();
        Managers.Popup.Close();
    }
}
