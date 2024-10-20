using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    private const float PROJECTILE_SPEED = 2;

    [SerializeField] private PatternSO startProjectile;
    private List<EnemySpawnData> projectileSpawnList;

    [SerializeField] public List<PatternSO> startEnemyList;
    private readonly Dictionary<EnemyType, List<Vector3>> startEnemyDic = new Dictionary<EnemyType, List<Vector3>>();

    private void Awake()
    {           
        projectileSpawnList = startProjectile.pattern.spawnPointList;

        SetStartEnemyDic();
    }

    private void Start()
    {
        StartCoroutine(CoSpawnProjectile());
    }

    private void SetStartEnemyDic()
    {
        foreach (var patternSO in startEnemyList)
        {
            List<EnemySpawnData> tempList = patternSO.pattern.spawnPointList;
            List<Vector3> posList = new List<Vector3>();

            for (int i = 0; i < tempList.Count; i++)
            {
                posList.Add(tempList[i].Pos);
            }

            startEnemyDic.Add(tempList[0].EnemyType, posList);
        }
    }

    public void SpawnStageEnemy(PatternSO patternSO)
    {
        List<EnemySpawnData> sqawnDataList = patternSO.pattern.spawnPointList;

        for (int i = 0; i < sqawnDataList.Count; i++)
        {
            GameObject enemy = ObjectPoolManager.Instance.GetObject(sqawnDataList[i].EnemyType.ToString());
            var posList = startEnemyDic[sqawnDataList[i].EnemyType];
            int randIdx = UnityEngine.Random.Range(0, posList.Count);

            enemy.transform.position = posList[randIdx];

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.SetEnemy(sqawnDataList[i].EnemyType);

            enemyController.SetDoMove(sqawnDataList[i].Pos);
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

        controller.Move(dir, PROJECTILE_SPEED);
    }
}