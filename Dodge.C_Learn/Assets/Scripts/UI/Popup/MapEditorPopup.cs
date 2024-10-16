using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapEditorPopup : BasePopup
{
    [SerializeField] private TMP_InputField inputX;
    [SerializeField] private TMP_InputField inputY;

    public override void Init()
    {
        base.Init();

        inputX.onEndEdit.AddListener(OnInput);
    }

    private void OnInput(string s)
    {
        SpawnPoint go = MapGenerator.Instance.ChoiceSpawnPoint();
    }

    public void CreatePoint()
    {
        MapGenerator.Instance.CreatePoint();
    }

    public void Save()
    {

    }

    public void Load()
    {
        
    }
}
