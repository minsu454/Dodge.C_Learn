using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float FireRate;
    public float projectileSpeed;

    public AttackSO attackSO;

    protected void SpawnBullet(ObjectType type, string curBullet, Vector3 pos, Vector2 dir)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(curBullet, transform, pos);
        ProjectileController projectTileController = bullet.GetComponent<ProjectileController>();
        projectTileController.myType = type;
        projectTileController.Shoot(dir * projectileSpeed);
    }
}
