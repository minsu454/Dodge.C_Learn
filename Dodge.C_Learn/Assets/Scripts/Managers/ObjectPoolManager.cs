using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;
    private ObjectContainer objectContainer;

    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("ObjectPoolManager");
                _instance = obj.AddComponent<ObjectPoolManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        objectContainer = gameObject.AddComponent<ObjectContainer>();
    }

    public void InitializePool(ObjectType key, GameObject prefab, int count)
    {
        objectContainer.CreateObject(key, prefab, count);
    }

    public GameObject GetObject(ObjectType key)
    {
        return objectContainer.GetObject(key);
    }

    public void ReturnObject(ObjectType key, GameObject obj)
    {
        objectContainer.ReturnObject(key, obj);
    }
}
