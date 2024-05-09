using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScipt : MonoBehaviour
{
    [SerializeField] Transform pieces;


    private float collapseSpeed = 3;


    [SerializeField] EnemySpawner es;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (es.maxEnemies > 0) return;
        ExecuteFirstLevelMove();
    }

    void ExecuteFirstLevelMove()
    {
        pieces.transform.Translate(Vector3.down * collapseSpeed * Time.deltaTime);
    }
}
