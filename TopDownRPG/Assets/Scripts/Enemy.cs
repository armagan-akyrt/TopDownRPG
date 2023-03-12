using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement
{
    // XP drop
    public int xpValue;

    public float trigLength = 1f;
    public float chaseLength = 5f;

    private bool chasing;
    private bool collideWithPlayer;
    private Transform playerTransform;
    [SerializeField] Vector3 startingPosition;

    // hitbox
    private ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];


    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startingPosition = transform.position;

        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // is the player in range
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < trigLength)
            {
                chasing = true;
            }

            if (chasing)
            {
                if (!collideWithPlayer)
                UpdateMotor((playerTransform.position - transform.position).normalized);
            }

        }
        else
        {
            UpdateMotor((startingPosition - transform.position).normalized);
            chasing = false;
        }

        // check overlaps
        collideWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].CompareTag("Player"))
            {
                collideWithPlayer = true;
            }

            //clear array
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.experience += xpValue;
        GameManager.instance.ShowText("+" + xpValue + "xp", 28, Color.yellow, transform.position, Vector3.up * 35, 1f);
    }

}
