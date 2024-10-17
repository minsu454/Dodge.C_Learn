using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    [Header("Prefab")]
    public Transform pointTr;
    public GameObject pointPrefab;
    private List<GameObject> pointPrefabList = new List<GameObject>();

    public MapController controller { get; private set; }

    private void Awake()
    {
        Instance = this;

        controller = GetComponent<MapController>();
    }

    private void Start()
    {
        Managers.Popup.CreatePopup(PopupType.MapEditorPopup);
    }

    public void SaveData()
    {

    }

    public void CreatePoint()
    {
        GameObject clone = Instantiate(pointPrefab, pointTr);

        pointPrefabList.Add(clone);
    }

    public void CreateBossPoint()
    {
            
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
