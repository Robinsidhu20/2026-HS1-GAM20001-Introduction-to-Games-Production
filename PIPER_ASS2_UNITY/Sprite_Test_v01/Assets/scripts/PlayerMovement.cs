using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform ground_check;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
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
    return Physics2D.OverlapCircle(ground_check.position, 0.2f, groundLayer);
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