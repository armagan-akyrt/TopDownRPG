using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
        {
            instance = this;
        }

    public List<Sprite> playerSprites;
    public List<RuntimeAnimatorController> animatorControllers;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Player player;
    public Weapon weapon;

    public FloatingTextManager floatingTextManager;

    public int money;
    public int experience;

    public int level;
    

    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, pos, motion, duration);
    }

    public void SwapCharacter(int charSprite)
    {

        player.changeCharacter(animatorControllers[charSprite], playerSprites[charSprite]);
        

    }


    // xp system
    public int GetCurrentLevel()
    {
        int ret = 0, sum = 0;

        while (experience >= sum)
        {
            sum += xpTable[ret];
            ret++;

            if (ret >= xpTable.Count)
            {
                return ret;
            }
        }

        level = ret;
        return ret;
    }

    public int Level2Xp(int level)
    {
        int temp = 0, xp = 0;

        while (temp <= level)
        {
            xp += xpTable[temp];
            temp++;
        }

        return xp;
    }

    public int CalculateNextLevelXp(int currentLevel)
    {
        int sum = 0;
        for (int i = 0; i < currentLevel; i++)
        {
            sum += xpTable[i];
        }

        return sum;
    }

    public float CalculateRatio()
    {
        int xpSofar = 0;
        for (int i = 0; i < level - 1; i++)
        {
            xpSofar += xpTable[i];
        }
        int diff = experience - xpSofar;

        return (float)diff / (float)xpTable[level - 1];

    }

    // upgrade weapon
    public bool TryWeaponUpgrade()
    {
        // is weapon max level?
        if (weaponPrices.Count - 1 <= weapon.weaponLevel)
        {
            return false;
        }

        if (money >= weaponPrices[weapon.weaponLevel])
        {
            money -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }
    

    /// <summary>
    /// INT preferredSkin
    /// INT cash
    /// INT XP
    /// INT weaponLevel
    /// </summary>
    public void SaveState()
    {
        string s = "";

        s += "0" + '|';
        s += money.ToString() + '|';
        s += experience.ToString() + '|';
        s += weapon.weaponLevel.ToString() + '|';
        

        PlayerPrefs.SetString("SaveState", s);


    }

    public void LoadState()
    {

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // TODO: Set player skin
        // TODO: Set player money
        money = int.Parse(data[1]);
        // Set XP
        experience = int.Parse(data[2]);
        // Set weapon level
        weapon.setWeaponLevel(int.Parse(data[3]));



        Debug.Log("LoadState");
    }
}
