using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.gObj = Instantiate(textPrefab);
            txt.gObj.transform.SetParent(textContainer.transform);
            txt.text = txt.gObj.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }

    public void Show(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.text.text = msg;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;

        floatingText.gObj.transform.position = Camera.main.WorldToScreenPoint(pos); // world space => screen space
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();

    }

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }
}
