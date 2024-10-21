using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoadScenePopup
{
    public SceneType nextScene { get; set; }
    public void LoadSceneAndClose();
}
