using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public float baseDmg = 1f, dmgVal = 1f;
    public float basePushForce = 2f, pushForce = 2f;

    // Weapon Upgrades
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    public float cooldown = .5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
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

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // stat upgrades.
        dmgVal = baseDmg + weaponLevel * .5f;
        pushForce = basePushForce + weaponLevel * .1f;
    }

    public void setWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // stat upgrades.
        dmgVal = baseDmg + weaponLevel * .5f;
        pushForce = basePushForce + weaponLevel * .1f;
    }
}
