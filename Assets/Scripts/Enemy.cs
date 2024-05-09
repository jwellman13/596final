using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This can serve as the shell for an enemy. The scriptable object allows for the easy creation for some
/// standard properties of the enemy without needing to modify the enemy prefab. This will be extended 
/// further once animations are done with an animation override controller to allow a single enemy prefab 
/// that can swap models on object construction.
/// </summary>
public class Enemy : MonoBehaviour, ITakeDamage
{

    [SerializeField] AudioSource hurtSFX;
    public SO_Enemy enemyData;

    private int health;
    protected int damage;
    protected float speed;

    // Flags
    protected bool isMarkedToDestroy = false;

    private void Awake()
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for death
        if (isMarkedToDestroy)
        {
            Debug.Log("Kill");
            Die();
        }
    }

    void Init()
    {
        health = enemyData.Health;
        damage = enemyData.Damage;
        speed = enemyData.Speed;
    }

    public bool TakeDamage(int amount)
    {
        Debug.Log("Ouch");
        health -= amount;
        if (health <= 0)
        {
            if (hurtSFX != null)
            {
                hurtSFX.Play();

            }
            isMarkedToDestroy = true;
        }
        return health > 0;
    }

    private void Die()
    {

        Destroy(gameObject, 0.5f);
        Debug.Log(name + " died!");
    }
}
