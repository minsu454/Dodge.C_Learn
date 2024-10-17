using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    [Header("Prefab")]
    public Transform pointTr;
    public GameObject pointPrefab;
    private List<SpawnPoint> pointPrefabList = new List<SpawnPoint>();

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

    /// <summary>
    /// json으로 데이터 직렬화해주는 함수
    /// </summary>
    public string DataToJson()
    {
        Patten spawnSO = new Patten();

        foreach (var spawnPoint in pointPrefabList)
        {
            EnemySpawnData enemySpawnData = new EnemySpawnData();
            enemySpawnData.EnemyType = spawnPoint.enemyType;
            enemySpawnData.Pos = spawnPoint.transform.position;

            spawnSO.spawnPointList.Add(enemySpawnData);
        }

        string s = JsonUtility.ToJson(spawnSO);

        return s;
    }

    /// <summary>
    /// json파일 주소 받아와서 역직렬화해주는 함수
    /// </summary>
    public void LoadData(string path)
    {
        string json = File.ReadAllText(path);
        Patten spawnSO = JsonUtility.FromJson<Patten>(json);

        try
        {
            foreach (var enemyPoint in pointPrefabList)
            {
                Destroy(enemyPoint.gameObject);
            }

            pointPrefabList.Clear();

            foreach (var spawnPoint in spawnSO.spawnPointList)
            {
                CreatePoint(spawnPoint);
            }
        }
        catch
        {
            throw new ArgumentNullException();
        }
    }

    /// <summary>
    /// 스폰 포인트 만들어주는 함수
    /// </summary>
    public void CreatePoint(EnemySpawnData data = null)
    {
        GameObject clone = Instantiate(pointPrefab, pointTr);
        SpawnPoint spawnPoint = clone.GetComponent<SpawnPoint>();

        if (data != null)
        {
            spawnPoint.enemyType = data.EnemyType;
            spawnPoint.transform.position = data.Pos;
        }

        pointPrefabList.Add(spawnPoint);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    internal SpawnPoint ChoiceSpawnPoint()
    {
        return controller.spawnPoint;
    }

    internal void Remove(SpawnPoint spawnPoint)
    {
        spawnPoint.SetOutline(false);
        Destroy(spawnPoint.gameObject);
        spawnPoint = null;
    }
}
