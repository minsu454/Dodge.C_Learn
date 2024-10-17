
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPoint : MonoBehaviour
{
    private SpriteOutline outline;       //외곽선
    private bool isFollow = false;

    private void Awake()
    {
        outline = GetComponent<SpriteOutline>();
    }

    public void Update()
    {
        if (!isFollow)
            return;

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    /// <summary>
    /// 외곽선 키고 끄는 함수
    /// </summary>
    public void SetOutline(bool enabled)
    {
        outline.enabled = enabled;
    }


    public void FollowMouse(bool isFollow)
    {
        this.isFollow = isFollow;
    }
}
