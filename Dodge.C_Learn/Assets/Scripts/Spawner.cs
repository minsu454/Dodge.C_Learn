
using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;


public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    [SerializeField] private PatternSO spawnProjectile;
    private List<EnemySpawnData> projectileSpawnList;

    //[SerializeField] public PatternSO spawnEnemy;
    //private List<EnemySpawnData> enemySpawnList;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();

        projectileSpawnList = spawnProjectile.pattern.spawnPointList;
        //enemySpawnList = spawnProjectile.pattern.spawnPointList;
    }

    private void Start()
    {
        StartCoroutine(CoSpawnProjectile());
    }

    public void SpawnStageEnemy(PatternSO patternSO)
    {
        List<EnemySpawnData> sqawnDataList = patternSO.pattern.spawnPointList;

        for (int i = 0; i < sqawnDataList.Count; i++)
        {
            GameObject enemy = ObjectPoolManager.Instance.GetObject(sqawnDataList[i].EnemyType.ToString());
            enemy.transform.position = sqawnDataList[i].Pos;
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.SetEnemy(sqawnDataList[i].EnemyType);
        }
    }

    IEnumerator CoSpawnProjectile()
    {
        while (true)
        {
            yield return YieldCache.WaitForSeconds(1f);

            GameObject projectile = ObjectPoolManager.Instance.GetObject("EnemyProjectile");
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            RandomShootInScreen(projectileController);
        }
    }

    private void RandomShootInScreen(ProjectileController controller)
    {
        float dirX = UnityEngine.Random.Range(0, Screen.width);
        float dirY = UnityEngine.Random.Range(0, Screen.height);

        int idx = UnityEngine.Random.Range(0, projectileSpawnList.Count);

        Vector2 pos = projectileSpawnList[idx].Pos;
        Vector2 dir = ((Vector2)Camera.main.ScreenToWorldPoint(new Vector2(dirX, dirY)) - pos).normalized;

        controller.transform.position = pos;

        controller.Shoot(dir);
    }
}