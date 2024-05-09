using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, ITakeDamage
{
    public bool TakeDamage(int amount)
    {
        Destroy(gameObject);
        return false;
    }

    
}
