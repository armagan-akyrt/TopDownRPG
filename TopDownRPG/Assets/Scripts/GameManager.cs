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

    public FloatingTextManager floatingTextManager;

    public int money;
    public int experience;

    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, pos, motion, duration);
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
        s += "0" + '|';


        PlayerPrefs.SetString("SaveState", s);


    }

    public void LoadState()
    {

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Set player skin
        // Set player money
        money = int.Parse(data[1]);
        // Set XP
        experience = int.Parse(data[2]);
        // Set weapon level

        Debug.Log("LoadState");
    }
}
