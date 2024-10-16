using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP : MonoBehaviour
{
    void Start()
    {
    }

    void Shoot()
    {

    }


    // 만약 충돌에서 처리하는 경우
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "??")
        {
            // ~~ rigidbody2D의 속력을 0으로!
            //ObjectPoolManager.Instance.ReturnObject(~~, this.gameObject);
        }
    }
}
