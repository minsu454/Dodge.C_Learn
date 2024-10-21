using Common.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAutoFalse : MonoBehaviour
{
    public bool isObjectPool = true;
    public float lifeTime = 1;

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
