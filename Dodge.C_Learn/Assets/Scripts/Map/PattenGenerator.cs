using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PattenGenerator : MonoBehaviour
{
    public static PattenGenerator Instance;

    [Header("Prefab")]
    public Transform pointTr;           //point부모위치
    public GameObject pointPrefab;      //프리팹위치
    private HashSet<SpawnPoint> pointPrefabList = new HashSet<SpawnPoint>();  //point 담아줄 list

    public SpawnPoint spawnPoint;                                    //현재 잡고있는 spawnPoint

    public PattenController controller { get; private set; }            //pattencontroller

    private void Awake()
    {
        Instance = this;

        controller = GetComponent<PattenController>();
    }

    private void Start()
    {
        Managers.Popup.CreatePopup(PopupType.MapEditorPopup);
    }

    /// <summary>
    /// json으로 데이터 직렬화해주는 함수
    /// </summary>
    public string DataToJson(string name)
    {
        Pattern patten = new Pattern();
        //patten
        patten.name = name;

        foreach (var spawnPoint in pointPrefabList)
        {
            EnemySpawnData enemySpawnData = new EnemySpawnData();
            enemySpawnData.EnemyType = spawnPoint.EnemyType;
            enemySpawnData.Pos = spawnPoint.transform.position;

            patten.spawnPointList.Add(enemySpawnData);
        }

        string s = JsonUtility.ToJson(patten);

        return s;
    }

    /// <summary>
    /// json파일 주소 받아와서 역직렬화해주는 함수
    /// </summary>
    public void LoadData(string path)
    {
        string json = File.ReadAllText(path);
        Pattern pattern = JsonUtility.FromJson<Pattern>(json);

        RemoveSpawnPoint();

        try
        {
            foreach (var enemyPoint in pointPrefabList)
            {
                Destroy(enemyPoint.gameObject);
            }

            pointPrefabList.Clear();

            foreach (var spawnPoint in pattern.spawnPointList)
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
            spawnPoint.EnemyType = data.EnemyType;
            spawnPoint.transform.position = data.Pos;
        }

        pointPrefabList.Add(spawnPoint);
    }

    /// <summary>
    /// spawnpoint 지워주는 함수
    /// </summary>
    public void RemoveSpawnPoint()
    {
        if (spawnPoint == null)
            return;

        spawnPoint.SetOutline(false);

        pointPrefabList.Remove(spawnPoint);
        Destroy(spawnPoint.gameObject);
        spawnPoint = null;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
