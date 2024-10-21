using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D projectileRb;
    public int Damage;                  //데미지
    public AttackerType myType;         //공격자의 타입

    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 투사체 움직이는 함수
    /// </summary>
    public void Move(Vector3 vec, float speed)
    {
        projectileRb.velocity = vec * speed;
    }

    /// <summary>
    /// 투사체 objectpool로 반환해주는 함수
    /// </summary>
    public void ReturnObject(SfxType type)
    {
        GameObject effect = ObjectPoolManager.Instance.GetObject("HitParticle");
        effect.transform.position = transform.position;
        Managers.Sound.PlaySFX(type);
        ObjectPoolManager.Instance.ReturnObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && myType == AttackerType.Enemy)
        {
            ReturnObject(SfxType.Hit_Player);
        }
        else if (collision.CompareTag("Enemy") && myType == AttackerType.Player)
        {
            ReturnObject(SfxType.Hit_Enemy);
        }
        else if (collision.CompareTag("Boarder"))
        {
            ObjectPoolManager.Instance.ReturnObject(gameObject);
        }
    }
}
