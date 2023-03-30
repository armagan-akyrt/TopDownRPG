using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Collidable
{
    protected bool collected;

    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);

        if (collisionWithPlayer)
        {
            
            OnHover();
            
        }
        
    }

    protected virtual void OnCollect()
    {

    }

    protected virtual void OnHover()
    {
        if (Input.GetAxisRaw("Interact") == 1)
        {
            OnCollect();
        }
    }

}
