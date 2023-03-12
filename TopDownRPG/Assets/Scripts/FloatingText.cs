using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject gObj;
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gObj.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        gObj.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }

        if (Time.time - lastShown > duration)
        {
            Hide();
        }


        gObj.transform.position += motion * Time.deltaTime;

    }
}
