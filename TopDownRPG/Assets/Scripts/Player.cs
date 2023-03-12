using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{
    private Animator anim;
    private float x, y;
    public float dashSpeed = 5f;
    public float dashCooldown = 3f;
    private bool canDash = true, isDashing = false;
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
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
            StartCoroutine(Dash());
        }

        UpdateMotor(new Vector3(x, y, 0));

        
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float gravOrig = rb.gravityScale;

        Vector2 direction = new Vector2(x, y);
        direction = direction.normalized;

        rb.gravityScale = 0f;
        rb.velocity = direction * dashSpeed;

        yield return new WaitForSeconds(dashCooldown);

        rb.gravityScale = gravOrig;
        isDashing = false;
        canDash = true;


    }
}
