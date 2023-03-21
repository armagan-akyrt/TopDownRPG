using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectible
{   
    private Sprite defaultChest;
    public Sprite hoveredChest;
    public Sprite emptyChest;
    public int lootAmount = 5;

    protected override void Start()
    {
        base.Start();
        defaultChest = GetComponent<SpriteRenderer>().sprite;
    }
    protected override void OnHover()
    {
        if (!collected)
        {
            
            GetComponent<SpriteRenderer>().sprite = hoveredChest;
            base.OnHover();
        }

        
        
    }
    protected override void OnCollect()
    {
        if (!collected)
        {
            GameManager.instance.ShowText("Press E to open.", 25, Color.blue, transform.position, Vector3.up * 50, 2.5f);
            GameManager.instance.money += 10;
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.LogWarning("COLLECTED");

        }
    }

}
