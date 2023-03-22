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
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Player player;
    public Weapon weapon;

    public FloatingTextManager floatingTextManager;

    public int money;
    public int experience;

    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, pos, motion, duration);
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
