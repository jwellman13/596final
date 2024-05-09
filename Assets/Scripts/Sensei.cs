using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensei : Enemy
{
    private bool isRight = false;
    private bool readyToSwap = true;
    private float swapCooldown = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMarkedToDestroy)
        {
            Destroy(gameObject);
        }
        if (isRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (readyToSwap)
        {
            StartCoroutine(Swap());
        }
        
    }

    IEnumerator Swap()
    {
        readyToSwap = false;
        yield return new WaitForSeconds(swapCooldown);

        isRight = !isRight;
        readyToSwap = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ITakeDamage>() != null)
        {
            collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(damage);
        }
    }
}
