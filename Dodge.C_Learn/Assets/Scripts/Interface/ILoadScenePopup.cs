using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버튼 누르면 로드씬이 되는 팝업 인터페이스
/// </summary>
public interface ILoadScenePopup
{
    public SceneType nextScene { get; set; }
    public void LoadSceneAndClose();
}
