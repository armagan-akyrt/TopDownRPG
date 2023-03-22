using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharMenu : MonoBehaviour
{
    // text fields
    public Text LevelText, hpText, cashText, upCostText, xpText;

    // logic 
    private int currentCharSelection = 0;
    public Image charSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // char selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharSelection++;

            // if end
            if (currentCharSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharSelection = 0;

            }

            OnSelectionChange();
        }

        else
        {
            currentCharSelection--;

            // if end
            if (currentCharSelection < 0)
            {
                currentCharSelection = GameManager.instance.playerSprites.Count - 1;

            }

            OnSelectionChange();
        }

        
    }

    private void OnSelectionChange()
    {
        charSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharSelection];
        GameManager.instance.SwapCharacter(currentCharSelection);
    }

    // weapon upgrade
    public void OnUpgradeClick()
    {
        // ref to weapon
        if (GameManager.instance.TryWeaponUpgrade())
        {
            UpdateMenu();
        }
    }

    public void UpdateMenu()
    {
        // weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        upCostText.text = "W.I.P.";

        // meta
        hpText.text = GameManager.instance.player.currentHealth.ToString();
        cashText.text = GameManager.instance.money.ToString();
        LevelText.text = "W.I.P.";

        // xp bar
        xpText.text = "W.I.P.";
        xpBar.localScale = new Vector3(.5f, 0, 0);

    }
}
