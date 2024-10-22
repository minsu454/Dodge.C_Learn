using Common.Timer;
using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    private const float PROJECTILE_SPEED = 2;                       //투사체 스피드

    [SerializeField] private PatternSO startProjectile;             //투사체 생성 SO
    private List<EnemySpawnData> projectileSpawnList;               //투사체 생성 리스트(위에 내용 풀기)

    [SerializeField] public List<PatternSO> startEnemyList;         //적 생성 SO 리스트
    private readonly Dictionary<EnemyType, List<Vector3>> startEnemyDic = new Dictionary<EnemyType, List<Vector3>>();   //적 생성 dictionary

    private void Awake()
    {           
        projectileSpawnList = startProjectile.pattern.spawnPointList;

        SetStartEnemyDic();
    }

    /// <summary>
    /// 적 생성 SO 풀어서 Dictionary에 저장해주는 함수
    /// </summary>
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

    /// <summary>
    /// 적 스폰 해주는 함수
    /// </summary>
    public void SpawnStageEnemy(Stage stageSO)
    {
        List<EnemySpawnData> sqawnDataList = stageSO.PatternList.pattern.spawnPointList;

        for (int i = 0; i < sqawnDataList.Count; i++)
        {
            GameObject enemy = ObjectPoolManager.Instance.GetObject("BaseEnemy");
            var posList = startEnemyDic[sqawnDataList[i].EnemyType];
            int randIdx = UnityEngine.Random.Range(0, posList.Count);

             enemy.transform.position = posList[randIdx];

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.SetEnemy(sqawnDataList[i].EnemyType);

            enemyController.SetDoMove(sqawnDataList[i].Pos);
        }

        StartCoroutine(CoTimer.Start(stageSO.DurationTime, () => Managers.Event.Dispatch(GameEventType.EnemyMoveTimerCompleted, Vector3.down)));
    }

    /// <summary>
    /// 투사체 발사 실행해주는 함수
    /// </summary>
    public void StartCoSpawnProjectile()
    {
        StartCoroutine(CoSpawnProjectile());
    }

    /// <summary>
    /// 투사체 발사 코루틴
    /// </summary>
    IEnumerator CoSpawnProjectile()
    {
        while (true)
        {
            yield return YieldCache.WaitForSeconds(0.5f);

            GameObject projectile = ObjectPoolManager.Instance.GetObject("EnemyProjectile");
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            RandomShootInScreen(projectileController);
        }
    }

    /// <summary>
    /// 랜덤 스크린 좌표를 따와서 투사체 발사하는 함수
    /// </summary>
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