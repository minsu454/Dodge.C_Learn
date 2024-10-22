using UnityEngine;

public class Shooter : MonoBehaviour
{
    protected AttackerType objType;         //공격한 자의 타입

    protected float furerateDelay = 0.1f;   //연사의 딜레이(n발 연속으로 쐈을 때)

    /// <summary>
    /// 투사체 발사해주는 함수
    /// </summary>
    protected void SpawnBullet(string curBullet, Vector3 pos, Vector2 dir, float speed)
    {
        GameObject bullet = ObjectPoolManager.Instance.GetObject(curBullet, transform, pos);

        ProjectileController projectTileController = bullet.GetComponent<ProjectileController>();
        projectTileController.myType = objType;
        projectTileController.Move(dir, speed);
    }
}
