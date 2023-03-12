using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public float dmgVal = 1f;
    public float pushForce = 2f;

    // Weapon Upgrades
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    public float cooldown = .5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            // create a new dmg object and send it to the enemy.
            Damage dmg = new Damage()
            {
                damageAmount = dmgVal,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("RecieveDamage", dmg);
            Debug.Log("Enemy hit");
        }
    }
}
