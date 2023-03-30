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
        LevelText.text = GameManager.instance.GetCurrentLevel().ToString();

        // xp bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        GameManager.instance.level = currLevel;
        if (currLevel >= GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + "total xp";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            /*int prevLevelXp = GameManager.instance.Level2Xp(currLevel - 1);
            int currLevelXp = GameManager.instance.Level2Xp(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;

            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff.ToString();*/

            int reqXpTotal = GameManager.instance.CalculateNextLevelXp(currLevel);
            float ratio = GameManager.instance.CalculateRatio();

            xpBar.localScale = new Vector3(ratio, 1, 1);
            xpText.text = GameManager.instance.experience.ToString() + "/" + reqXpTotal.ToString();
        }


    }
}
