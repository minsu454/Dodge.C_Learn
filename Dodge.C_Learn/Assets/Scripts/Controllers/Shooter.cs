using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float FireRate;
    public float projectileSpeed;
    public AttackerType objType;

    public BaseInfoSO attackSO;

    protected void SpawnBullet(string curBullet, Vector3 pos, Vector2 dir)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(curBullet, transform, pos);
        ProjectileController projectTileController = bullet.GetComponent<ProjectileController>();
        projectTileController.myType = objType;
        projectTileController.Shoot(dir * projectileSpeed);
    }
}
