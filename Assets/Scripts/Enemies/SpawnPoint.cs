using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private int totalValue;

    [SerializeField] private float maxSpawnDistanceX;
    [SerializeField] private float maxSpawnDistanceYTop;
    [SerializeField] private float maxSpawnDistanceYBot;
    [SerializeField] private float spawnCheckDiamater;
    [SerializeField] private LayerMask spawnCheckMask;

    [SerializeField] private float roamArea;

    private int loops = 0;

    private void Start()
    {
        maxSpawnDistanceX = roamArea;

        enemies.Sort((left, right) => left.GetComponent<GeneralEnemyScript>().value.CompareTo(right.GetComponent<GeneralEnemyScript>().value));

        while (totalValue > 0 && loops < 100)
        {
            loops++;
            GameObject curEnemy;

            if (totalValue > enemies.Count)
            {
                curEnemy = enemies[Random.Range(0, enemies.Count - 1)];
            }
            else
            {
                curEnemy = enemies[Random.Range(0, totalValue)];
            }

            GeneralEnemyScript curEnemyScript = curEnemy.GetComponent<GeneralEnemyScript>();

            totalValue -= curEnemyScript.value;
            curEnemyScript.roamPoint = gameObject;

            int dir = Random.Range(0, 2);
            if (dir == 0)
                dir = -1;

            curEnemyScript.moveDirection = dir;

            GeneralEnemyScript enemyScript = Instantiate(curEnemy, FindSpawn(), Quaternion.identity).GetComponent<GeneralEnemyScript>();

            if (curEnemyScript.value != 5)
                enemyScript.roamMaxDist = roamArea;
        }
    }

    private Vector3 FindSpawn()
    {
        Vector3 checkedSpawn = Vector3.zero;
        while (checkedSpawn == Vector3.zero)
        {
            Vector3 potSpawn = new Vector3(transform.position.x + Random.Range(-maxSpawnDistanceX, maxSpawnDistanceX), transform.position.y + Random.Range(maxSpawnDistanceYBot, maxSpawnDistanceYTop), 0);

            if (!Physics2D.OverlapCircle(potSpawn, spawnCheckDiamater, spawnCheckMask))
            {
                checkedSpawn = potSpawn;
            }
        }
        return checkedSpawn;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, roamArea);
    }
}
