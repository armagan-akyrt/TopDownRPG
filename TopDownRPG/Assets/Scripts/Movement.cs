using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : Fighter
{
    protected BoxCollider2D boxCollider; // enemy box collider

    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    

    protected float ySpeed = .75f;
    protected float xSpeed = 1f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameManager.instance.LoadState();
        boxCollider = GetComponent<BoxCollider2D>();
    }



    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed);

        // Swap sprite direction based on horizontal direction
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Make player move in y direction.
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        // Make player move in y direction.
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
    }
}
