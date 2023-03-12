using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Collidable : MonoBehaviour
{

    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    protected bool collisionWithPlayer = false;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    // Update is called once per frame
    protected virtual void Update()
    {

        if (hits.Length == 0)
        {
            collisionWithPlayer = false;
        }
        // collision work
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            OnCollide(hits[i]);

            //clear array
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        collisionWithPlayer = (coll.name == "Player") ? true : false;
    }
}
