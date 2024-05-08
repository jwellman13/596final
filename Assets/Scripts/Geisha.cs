using UnityEngine;

public class Geisha : MonoBehaviour
{
    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] float moveSpeed = 2f; // Speed at which the enemy moves
    [SerializeField] float paceDistance = 5f; // Distance the enemy paces back and forth

    private Rigidbody rb;
    private bool isGrounded;
    private bool isMovingRight = true; // Flag to track movement direction
    private Vector3 startPos; // Starting position of the enemy

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position; // Store the starting position
    }

    private void FixedUpdate()
    {
        // Perform raycast to check if the enemy is grounded
        isGrounded = Physics.Raycast(groundCheckPos.position, Vector3.down, groundCheckDistance, groundLayer);

        // Move the enemy back and forth along the x-axis
        if (isGrounded)
        {
            Vector3 movement = isMovingRight ? Vector3.right : Vector3.left;
            rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

            // Check if the enemy reached the end of its movement range
            if (Mathf.Abs(transform.position.x - startPos.x) >= paceDistance / 2)
            {
                // Change movement direction
                isMovingRight = !isMovingRight;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision logic if needed

    }
}
