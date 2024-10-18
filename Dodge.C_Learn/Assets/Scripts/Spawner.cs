
using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;


public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    //public Pattern();

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
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
            yield return YieldCache.WaitForSeconds(0.1f);

            GameObject projectile = ObjectPoolManager.Instance.GetObject("EnemyProjectile");
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectile.transform.position = spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length)].position;
            projectileController.RandomShoot();

        }
    }
}