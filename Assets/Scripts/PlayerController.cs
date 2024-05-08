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
    [SerializeField] float dashingForce = 10f;
    [SerializeField] float dashingTime = 0.5f;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float weaponCooldown = 0.3f;

    // Assigned in editor
    [SerializeField] Transform groundCheckPos;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject kunai;
    [SerializeField] Transform firePos;
    [SerializeField] AudioSource jumpSFX;
    [SerializeField] AudioSource dashSFX;
    [SerializeField] AudioSource footstepsSFX;


    // Internal variables
    private Rigidbody rb;
    private Animator anim;
    private float horizontalMove;


    // State-based checking
    private bool isFacingRight = false;
    private bool isGrounded;
    private bool isJumpButtonPressed;
    private bool canDash = true;
    private bool isDashing = false;
    private bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) return;

        if (Input.GetKey(KeyCode.E) && canFire)
        {
            StartCoroutine(Fire());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        horizontalMove = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Sets the player rotation to face the correct direction
        if (horizontalMove != 0)
        {
            float setFacing = Mathf.Atan2(horizontalMove, 0) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, setFacing, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (setFacing < 0)
            {
                isFacingRight = false;
            }
            else
            {
                isFacingRight = true;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || extraJumps > 0)
            {
                anim.SetBool("Grounded", false);
                isJumpButtonPressed = true;
            }
        }

        if (isGrounded)
        {
            anim.SetBool("Grounded",true);
            extraJumps = 1;
        }

        if (isGrounded && horizontalMove != 0)
        {
            footstepsSFX.enabled = true;
        }
        else
        {
            footstepsSFX.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        {

        }
        isGrounded = Physics.Raycast(groundCheckPos.position, Vector3.down, groundCheckDistance, groundLayer);
        rb.velocity = new Vector3(horizontalMove * playerSpeed, rb.velocity.y, 0);

        if (isJumpButtonPressed && extraJumps > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, 0);
            anim.SetTrigger("JumpTrigger");
            jumpSFX.Play();
            extraJumps--;
            isJumpButtonPressed = false;
            
        }
    }



    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.useGravity = false;
        float dir = 1f;
        if (!isFacingRight)
        {
            dir = -1f;
        }
        rb.velocity = new Vector2(transform.localScale.x * dashingForce * dir, 0f);

        dashSFX.Play();

        yield return new WaitForSeconds(dashingTime);

        rb.useGravity = true;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator Fire()
    {
        Debug.Log("Fire");
        canFire = false;
        GameObject go = Instantiate(kunai, firePos.position, Quaternion.identity);
        Projectile proj = go.GetComponent<Projectile>();
        proj.Fire(transform.forward);
        proj.SetFacing(isFacingRight);


        yield return new WaitForSeconds(weaponCooldown);

        canFire = true;
    }
}
