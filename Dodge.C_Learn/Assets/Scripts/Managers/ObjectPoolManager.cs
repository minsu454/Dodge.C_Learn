using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private ObjectContainer objectContainer;
    public GameObject temp;

    private void Awake()
    {
        objectContainer = gameObject.AddComponent<ObjectContainer>();
        //objectContainer = new ObjectContainer();
        InitializePool(ObjectType.Object, temp, 5);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetObject(ObjectType.Object);
        }
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
