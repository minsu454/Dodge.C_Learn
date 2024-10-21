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
            string objName = objectPrefabs[i].GO.gameObject.name;
            InitializePool(objName, objCase.GO, objCase.Count);
        }
    }

    public void InitializePool(string key, GameObject prefab, int count)
    {
        objectContainer.CreateObject(key, prefab, count);
    }

    public GameObject GetObject(string key)
    {
        return objectContainer.GetObject(key);
    }

    public GameObject GetObject(string key, Transform transform, Vector3 vec)
    {
        GameObject GO = objectContainer.GetObject(key);
        GO.transform.position = transform.position + vec;
        GO.transform.rotation = transform.rotation;

        return GO;
    }

    public void ReturnObject(GameObject obj)
    {
        objectContainer.ReturnObject(obj);
    }
}
