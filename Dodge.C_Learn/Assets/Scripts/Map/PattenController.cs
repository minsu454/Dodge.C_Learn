using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PattenController : MonoBehaviour
{
    private Camera mainCamera;
    private PlayerInput input;      //input

    private SpawnPoint spawnPoint;  //내가 잡고있는 spawnPoint

    public event Action<SpawnPoint> OnSpawn;        //스폰할때 실행 하는 action
    public event Action<SpawnPoint> OnMove;         //마우스 움직일때 실행 하는 action

    private bool isInputMouseLeftClick = false;         //마우스 클릭한 상태를 받는 변수
    private bool isStayCameraView = false;              //카메라 잠궈줄건지 받는 변수

    private float scrollLimit = 10;                     //카메라 size 리미트

    private void Awake()
    {
        mainCamera = Camera.main;
        input = GetComponent<PlayerInput>();

        Managers.Event.Subscribe(GameEventType.LockInput, OnLockInput);
        Managers.Event.Subscribe(GameEventType.StayCameraView, OnStayCameraView);
    }

    private void Update()
    {
        if (!isInputMouseLeftClick)
            return;

        OnMove?.Invoke(spawnPoint);
    }

    /// <summary>
    /// Input System을 잠구는 함수
    /// </summary>
    public void OnLockInput(object args)
    {
        bool isActive = (bool)args;
        input.enabled = isActive;
    }

    /// <summary>
    /// Input System을 잠구는 함수
    /// </summary>
    public void OnStayCameraView(object args)
    {
        bool isActive = (bool)args;
        isStayCameraView = isActive;
    }

    /// <summary>
    /// 클릭 버튼 action
    /// </summary>
    public void OnClick(InputValue value)
    {
        if (!value.isPressed)
        {
            spawnPoint?.FollowMouse(false);
            isInputMouseLeftClick = false;
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

        isInputMouseLeftClick = true;

        spawnPoint = point;
        OnSpawn.Invoke(spawnPoint);
    }

    /// <summary>
    /// 삭제 버튼 action
    /// </summary>
    public void OnDelete(InputValue value)
    {
        if (spawnPoint == null)
            return;

        PattenGenerator.Instance.Remove(spawnPoint);
    }

    /// <summary>
    /// 마우스 스크롤 action
    /// </summary>
    public void OnMouseScrollY(InputValue value)
    {
        if (isStayCameraView)
            return;

        float y = value.Get<float>();
        float size = mainCamera.orthographicSize - y;

        if (size <= 4 || scrollLimit < size)
            return;

        mainCamera.orthographicSize -= y;
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
}
