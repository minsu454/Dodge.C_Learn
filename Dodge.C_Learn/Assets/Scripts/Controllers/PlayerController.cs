using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    PlayerShooter shooter;
    bool isHit;
    /// <summary>
    /// 초기화 Awake : Rigidbody 가져오기
    /// </summary>
    private void Awake()
    {
        shooter = GetComponent<PlayerShooter>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().normalized;
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = moveInput.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position +  nextVec);
    }
    void OnHit()
    {
        shooter.Power --;
        if (shooter.Power <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Upgrade()
    {
        shooter.Power++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("EnemyProjectile"))
        {
            if(!isHit)
            OnHit();
        }
    }
}
