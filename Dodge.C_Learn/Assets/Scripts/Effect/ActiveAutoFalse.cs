using Common.Timer;
using UnityEngine;

public class ActiveAutoFalse : MonoBehaviour
{
    public bool isObjectPool = true;    // ObjectPool 이용하는지 체크변수
    public float lifeTime = 1;          //지속시간

    private void OnEnable()
    {
        StartCoroutine(CoTimer.Start(lifeTime, () => gameObject.SetActive(false)));
    }

    private void OnDisable()
    {
        if (isObjectPool)
        {
            ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
    }
}