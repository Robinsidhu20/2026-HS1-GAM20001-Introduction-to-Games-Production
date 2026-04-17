using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool canJump;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform ground_check;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSoundInterval = 0.25f;

    private float nextMoveSoundTime;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogWarning("PlayerMovement: Animator not assigned and not found on the GameObject.", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (animator != null)
        {
            animator.SetBool("isMoving", horizontal != 0f);
        }

        if (isGrounded())
        {
            canJump = true;
        }

        if (Mathf.Abs(horizontal) > 0f && isGrounded() && Time.time >= nextMoveSoundTime)
        {
            nextMoveSoundTime = Time.time + moveSoundInterval;
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayMoveSound();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            SoundManager.instance.PlayJumpSound();
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0.2f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
    }


private void FixedUpdate ()
{
    rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
}


private bool isGrounded()
{
    Collider2D[] hits = Physics2D.OverlapCircleAll(ground_check.position, groundCheckRadius, groundLayer);
    foreach (Collider2D hit in hits)
    {
        if (hit != null && hit.gameObject != gameObject)
        {
            return true;
        }
    }
    return false;
}

private void Flip()
{
    if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
    {
            isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
}