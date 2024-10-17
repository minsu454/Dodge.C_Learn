using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapController : MonoBehaviour
{
    private SpawnPoint spawnPoint;
    private Camera mainCamera;

    public Action<SpawnPoint> OnMove;
    private bool isInputMouseScroll = false;

    private float scrollLimit = 10;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!isInputMouseScroll)
            return;

        OnMove?.Invoke(spawnPoint);
    }

    public void OnClick(InputValue value)
    {
        if (!value.isPressed)
        {
            spawnPoint?.FollowMouse(false);
            isInputMouseScroll = false;
            return;
        }

        if (!IsMouseHit(out RaycastHit2D hit))
        {
            return;
        }

        SpawnPoint point = hit.collider.GetComponent<SpawnPoint>();

        if (point != spawnPoint)
        {
            spawnPoint?.SetOutline(false);
        }

        isInputMouseScroll = true;

        spawnPoint = point;
        spawnPoint.SetOutline(true);
        spawnPoint.FollowMouse(true);
    }

    public bool IsMouseHit(out RaycastHit2D hit)
    {
        Vector2 vec = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        hit = Physics2D.Linecast(vec, vec * 5);

        if (!hit)
            return false;

        return true;
    }

    public void OnDelete(InputValue value)
    {
        if (spawnPoint == null)
            return;

        spawnPoint.SetOutline(false);
        Destroy(spawnPoint.gameObject);
        spawnPoint = null;
    }

    public void OnMouseScrollY(InputValue value)
    {
        float v = value.Get<float>();
        float size = mainCamera.orthographicSize - v;

        if (size <= 0 || scrollLimit < size)
            return;

        mainCamera.orthographicSize -= v;
    }
}
