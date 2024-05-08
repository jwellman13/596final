using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(-25.0f, 0.0f, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
       
    }
}
