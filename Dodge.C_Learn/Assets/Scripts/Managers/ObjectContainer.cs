using System.Collections.Generic;
using UnityEngine;

public class ObjectContainer : MonoBehaviour
{
    private Dictionary<ObjectType, Queue<GameObject>> objContainer = new Dictionary<ObjectType, Queue<GameObject>>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject GetObject(ObjectType key)
    {
        if (!objContainer.ContainsKey(key))
        {
            return null; // 사용할 수 있는 오브젝트가 없음
        }

        GameObject obj = objContainer[key].Peek();

        // 마지막 하나가 남았을 때 복제해서 넣어준다.
        if (objContainer[key].Count == 1)
        {
            //GameObject duplicatedObj = Object.Instantiate(obj);
            obj = Object.Instantiate(obj);
        }
        else
        {
            objContainer[key].Dequeue();
        }

    
        obj.SetActive(true); // 사용하기 위해 활성화
        return obj;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    public void ReturnObject(ObjectType key, GameObject obj)
    {
        obj.SetActive(false); // 비활성화
        if (!objContainer.ContainsKey(key))
        {
            objContainer[key] = new Queue<GameObject>();
        }
        objContainer[key].Enqueue(obj);
    }

    /// <summary>
    /// Object를 instantiate 하여 Queue를 초기화합니다.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="prefab"></param>
    /// <param name="count"></param>
    public void CreateObject(ObjectType key, GameObject prefab, int count)
    {
        if (!objContainer.ContainsKey(key))
        {
            objContainer[key] = new Queue<GameObject>();
        }

        for (int i = 0; i < count; i++)
        {
            Debug.Log("hello");

            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false); // 비활성화 상태로 생성
            objContainer[key].Enqueue(obj);
        }
    }
}
