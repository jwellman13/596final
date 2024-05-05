using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Available in editor
    [SerializeField] float projectileSpeed = 5.0f;
    [SerializeField] int damage = 5;

    // Internal variables
    private Rigidbody rb;
    private Vector3 direction;
    private float ttl = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = projectileSpeed * direction;
    }

    public void Fire(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ITakeDamage>() != null)
        {
            ITakeDamage takeDamage = collision.gameObject.GetComponent<ITakeDamage>();
            takeDamage.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void SetFacing(bool isFacingRight)
    {
        if (!isFacingRight)
        {
            Vector3 flip = new Vector3(0, 180, 0);
            transform.eulerAngles = flip;
        }
    }

    IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }
}
