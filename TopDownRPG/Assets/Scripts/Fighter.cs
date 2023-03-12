using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float currentHealth = 10f;
    public float maxHealth = 10f;
    public float pushRecoverySpeed = .25f;
    protected Rigidbody2D rb;

    // Immunity
    protected float immuneTime = 1f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void RecieveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            currentHealth -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            rb.AddForce(new Vector2(pushDirection.x, pushDirection.y), ForceMode2D.Impulse);

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, .35f);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }

}
