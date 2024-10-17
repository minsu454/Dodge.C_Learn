using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapController : MonoBehaviour
{
    public SpawnPoint spawnPoint { get; set; }
    private Camera mainCamera;

    private PlayerInput input;

    public event Action<SpawnPoint> OnMove;
    private bool isInputMouseScroll = false;

    private float scrollLimit = 10;

    private void Awake()
    {
        mainCamera = Camera.main;
        input = GetComponent<PlayerInput>();
        Managers.Event.Subscribe(GameEventType.LockInput, OnLockInput);
    }

    private void Update()
    {
        if (!isInputMouseScroll)
            return;

        OnMove?.Invoke(spawnPoint);
    }

    /// <summary>
    /// Input을 잠구는 함수
    /// </summary>
    public void OnLockInput(object args)
    {
        bool isActive = (bool)args;
        input.enabled = isActive;
    }

    /// <summary>
    /// 클릭 버튼 action
    /// </summary>
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

    /// <summary>
    /// 마우스 클릭방향으로 레이를 쏴서 spawnpoint가 있는지 체크하는 함수 
    /// </summary>
    public bool IsMouseHit(out RaycastHit2D hit)
    {
        Vector2 vec = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        hit = Physics2D.Linecast(vec, vec * 5);

        if (!hit)
            return false;

        return true;
    }

    /// <summary>
    /// 삭제 버튼 action
    /// </summary>
    public void OnDelete(InputValue value)
    {
        if (spawnPoint == null)
            return;

        MapGenerator.Instance.Remove(spawnPoint);
    }

    /// <summary>
    /// 마우스 스크롤 action
    /// </summary>
    public void OnMouseScrollY(InputValue value)
    {
        float v = value.Get<float>();
        float size = mainCamera.orthographicSize - v;

        if (size <= 0 || scrollLimit < size)
            return;

        mainCamera.orthographicSize -= v;
    }
}
