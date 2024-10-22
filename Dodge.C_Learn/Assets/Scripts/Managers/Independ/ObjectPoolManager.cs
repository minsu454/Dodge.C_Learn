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

    /// <summary>
    /// ObjectPool 처음에 세팅해주는 함수
    /// </summary>
    private void PoolSetting()
    {
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            ObjectCase objCase = objectPrefabs[i];
            string objName = objectPrefabs[i].GO.gameObject.name;
            InitializePool(objName, objCase.GO, objCase.Count);
        }
    }

    /// <summary>
    /// ObjectPoolContainer에 추가해주는 함수
    /// </summary>
    public void InitializePool(string key, GameObject prefab, int count)
    {
        objectContainer.CreateObject(key, prefab, count);
    }

    /// <summary>
    /// ObjectPoolContainer에서 객체 가져오는 함수
    /// </summary>
    public GameObject GetObject(string key)
    {
        return objectContainer.GetObject(key);
    }

    /// <summary>
    /// ObjectPoolContainer에서 객체 가져오는 함수
    /// </summary>
    public GameObject GetObject(string key, Transform transform, Vector3 vec)
    {
        GameObject GO = objectContainer.GetObject(key);
        GO.transform.position = transform.position + vec;
        GO.transform.rotation = transform.rotation;

        return GO;
    }

    /// <summary>
    /// ObjectPoolContainer에 반납하는 함수
    /// </summary>
    public void ReturnObject(GameObject obj)
    {
        objectContainer.ReturnObject(obj);
    }
}
