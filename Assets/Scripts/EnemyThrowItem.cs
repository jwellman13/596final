using UnityEngine;

public class EnemyThrowItem : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public GameObject itemPrefab; // Prefab of the item to throw
    public Transform throwPosition; // Position from which the enemy will throw the item
    public float throwDistance = 5f; // Distance threshold for throwing the item
    public float throwForce = 10f; // Force to throw the item

    private bool hasThrown = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the player is within the throw distance and the item has not been thrown yet
        if (Vector3.Distance(transform.position, player.position) <= throwDistance && !hasThrown)
        {
            // Throw the item
            ThrowItem();
        }
    }

    void ThrowItem()
    {
        // Instantiate the item prefab at the specified throw position
        GameObject item = Instantiate(itemPrefab, throwPosition.position, Quaternion.identity);

        // Calculate the direction to throw the item
        Vector3 throwDirection = (player.position - throwPosition.position).normalized;

        // Apply force to the item to throw it towards the player
        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }

        // Set hasThrown to true to prevent continuous throwing
        hasThrown = true;

        // Destroy the item if it touches the floor or the player
        DestroyItemAfterDelay(item);
    }

    void DestroyItemAfterDelay(GameObject item)
    {
        StartCoroutine(DestroyItemCoroutine(item));
    }

    System.Collections.IEnumerator DestroyItemCoroutine(GameObject item)
    {
        // Wait for a short delay
        yield return new WaitForSeconds(0.5f);

        // Destroy the item
        Destroy(item);

        // Reset the hasThrown flag after the item is destroyed
        hasThrown = false;
    }
}
