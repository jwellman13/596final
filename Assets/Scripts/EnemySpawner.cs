using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Available in editor
    [SerializeField] float spawnCooldown = 3.0f;
    public int maxEnemies = 5;

    // Assigned in editor
    [SerializeField] GameObject enemyPrefab;

    // Internal variables
    bool canSpawn = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && maxEnemies > 0)
        {
            StartCoroutine(SpawnEnemy());
            maxEnemies--;

        }
    }

    IEnumerator SpawnEnemy()
    {
        canSpawn = false;
        GameObject go = Instantiate(enemyPrefab,transform.position, Quaternion.identity);


        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
