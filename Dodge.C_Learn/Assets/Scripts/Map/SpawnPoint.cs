
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPoint : MonoBehaviour
{
    private SpriteOutline outline;       //외곽선
    private bool isFollow = false;

    public EnemyType enemyType = EnemyType.Easy;

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

    /// <summary>
    /// 마우스 따라다닐건지 설정하는 함수
    /// </summary>
    public void FollowMouse(bool isFollow)
    {
        this.isFollow = isFollow;
    }
}
