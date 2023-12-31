using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] GameObject enemy;
    List<GameObject> enemyList;
    [SerializeField] int enemyListCount;
    [SerializeField] List<Transform> checkPointList;
    //[SerializeField] int spawnPointCount;
    [SerializeField] float timeToSpawn;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void Start()
    {
        enemyList = new List<GameObject>(enemyListCount);
        //spawnPointList = new List<Transform>(spawnPointCount);
 
        for (int i = 0; i < enemyListCount; i++)
        {
            int random = Random.Range(0, checkPointList.Count);
            Transform pointA = checkPointList[random];
            Transform pointB = checkPointList[(random + 1) % checkPointList.Count];
            GameObject tempEnemy = Instantiate(enemy, pointA.position, pointA.rotation);
            enemyList.Add(tempEnemy);
            enemyList[i].GetComponent<Move>().pointA = pointA; 
            enemyList[i].GetComponent<Move>().pointB = pointB;
            enemyList[i].SetActive(false);
           }
    }

    IEnumerator SpawnEnemies()
    {
        if (enemyList.Count > 0)
            for (int i = 0; i < enemyListCount; i++)
                enemyList[i].SetActive(true);
        yield return new WaitForSeconds(timeToSpawn);
    }


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnEnemies());
    }
}
