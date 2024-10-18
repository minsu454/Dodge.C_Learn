using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ObjectContainer : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> objContainer = new Dictionary<string, Queue<GameObject>>();

    /// <summary>
    /// 특정 ObjectType의 Pool에서 GameObject 하나를 꺼내옵니다.
    /// </summary>
    public GameObject GetObject(string key)
    {
        if (!objContainer.ContainsKey(key))
        {
            return null; // 사용할 수 있는 오브젝트가 없음
        }

        GameObject obj = objContainer[key].Peek();

        // 마지막 하나가 남았을 때 복제해서 넣어준다.
        if (objContainer[key].Count == 1)
        {
            obj = Object.Instantiate(obj);
        }
        else
        {
            objContainer[key].Dequeue();
        }

        Debug.Log(objContainer[key].Count);
        obj.SetActive(true); // 사용하기 위해 활성화
        return obj;
    }

    /// <summary>
    ///특정 ObjectType의 Pool에 GameObject 하나를 반환합니다.
    /// </summary>
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 비활성화
        if (!objContainer.ContainsKey(obj.name))
        {
            objContainer[obj.name] = new Queue<GameObject>();
        }
        objContainer[obj.name].Enqueue(obj);
    }

    /// <summary>
    /// 특정 ObjectType의 Object를 instantiate 하여 Pool(Queue)를 초기화합니다.
    /// </summary>
    public void CreateObject(string key, GameObject prefab, int count)
    {
        if (!objContainer.ContainsKey(key))
        {
            objContainer[key] = new Queue<GameObject>();
        }

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false); // 비활성화 상태로 생성
            objContainer[key].Enqueue(obj);
        }
    }
}
