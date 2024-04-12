using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Available in editor
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float rotationSpeed = 20.0f;
    [SerializeField] float jumpMagnitude = 2.0f;
    [SerializeField] int extraJumps = 1;
    [SerializeField] float groundCheckDistance = 0.1f;

    // Assigned in editor
    [SerializeField] Transform groundCheckPos;
    [SerializeField] LayerMask groundLayer;


    // Internal variables
    private Rigidbody rb;
    private float horizontalMove;


    // State-based checking
    private bool isGrounded;
    private bool isJumpButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        // Sets the player rotation to face the correct direction
        if (horizontalMove != 0)
        {
            float setFacing = Mathf.Atan2(horizontalMove, 0) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, setFacing, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || extraJumps > 0)
            {
                isJumpButtonPressed = true;
            }
        }

        if (isGrounded)
        {
            extraJumps = 1;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(groundCheckPos.position, Vector3.down, groundCheckDistance, groundLayer);
        rb.velocity = new Vector3(horizontalMove * playerSpeed, rb.velocity.y, 0);

        if (isJumpButtonPressed && extraJumps > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, 0);
            extraJumps--;
            isJumpButtonPressed = false;
        }
    }
}
