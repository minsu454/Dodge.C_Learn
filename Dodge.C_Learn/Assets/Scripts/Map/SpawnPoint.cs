
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPoint : MonoBehaviour
{
    private SpriteRenderer sprRenderer; 
    private SpriteOutline outline;      //외곽선
    private bool isFollow = false;      //마우스따라오는지

    private EnemyType enemyType = EnemyType.Corvette01;
    public EnemyType EnemyType
    {
        get { return enemyType; }
        set
        {
            if (enemyType != value)
            {
                sprRenderer.sprite = Managers.Character.ReturnSprite(value);
                enemyType = value;
            }
        }
    }

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
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
