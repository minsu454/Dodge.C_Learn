using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
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
}
