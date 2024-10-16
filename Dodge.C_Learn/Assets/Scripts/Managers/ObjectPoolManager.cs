using UnityEditor.SceneManagement;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;
    private ObjectContainer objectContainer;

    [Header("Prefabs")]
    [SerializeField] ObjectCase[] objectPrefabs;

    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        objectContainer = gameObject.AddComponent<ObjectContainer>();

        PoolSetting();
    }

    private void PoolSetting()
    {
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            ObjectCase objCase = objectPrefabs[i];
            InitializePool(objCase.Type, objCase.GO, objCase.Count);
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

    public GameObject GetObject(ObjectType key, Transform transform, Vector3 vec)
    {
        GameObject GO = objectContainer.GetObject(key);
        GO.transform.position = transform.position + vec;
        GO.transform.rotation = transform.rotation;
        return GO;
    }
    public void ReturnObject(ObjectType key, GameObject obj)
    {
        objectContainer.ReturnObject(key, obj);
    }
}
