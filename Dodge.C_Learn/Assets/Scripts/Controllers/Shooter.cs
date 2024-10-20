using UnityEngine;

public class Shooter : MonoBehaviour
{
    protected AttackerType objType;

    protected float furerateDelay = 0.1f;

    protected void SpawnBullet(string curBullet, Vector3 pos, Vector2 dir, float speed)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(curBullet, transform, pos);

        ProjectileController projectTileController = bullet.GetComponent<ProjectileController>();
        projectTileController.myType = objType;
        projectTileController.Move(dir, speed);
    }
}
