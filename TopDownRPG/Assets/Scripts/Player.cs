using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{
    private Animator anim;
    private float x, y;
    public float dashSpeed = 5f;
    public float dashDuration = .5f;
    public float dashCooldown = 2f;
    private bool canDash = true, isDashing = false;
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public void changeCharacter(RuntimeAnimatorController animcontroller, Sprite playerSprite)
    {
        GetComponent<SpriteRenderer>().sprite = playerSprite;
        anim.runtimeAnimatorController = animcontroller;

    }

    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0)
        {
            anim.SetBool("isMoving", true);

        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash(x, y));
        }

        UpdateMotor(new Vector3(x, y, 0));


    }

    private IEnumerator Dash(float x, float y)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is not attached to GameObject.");
            yield break;
        }

        canDash = false;
        isDashing = true;

        // Calculate movement based on x and y input parameters
        Vector2 movement = new Vector2(x, y).normalized * dashSpeed;

        // Store the current gravity scale to restore it later
        float originalGravityScale = rb.gravityScale;

        // Disable gravity during dash
        rb.gravityScale = 0f;

        // Apply movement velocity
        rb.velocity = movement;

        yield return new WaitForSeconds(dashDuration);

        // Stop the movement velocity
        rb.velocity = Vector2.zero;

        // Restore the original gravity scale
        rb.gravityScale = originalGravityScale;

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
