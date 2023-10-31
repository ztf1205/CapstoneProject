using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();

        EventManager.Subscribe("ChangeToGreen", ChangeToGreen);
        EventManager.Subscribe("ChangeToRed", ChangeToRed);
    }

    private void ChangeToGreen()
    {
        img.color = Color.green;
    }

    private void ChangeToRed()
    {
        img.color = Color.red;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("ChangeToGreen", ChangeToGreen);
        EventManager.Unsubscribe("ChangeToRed", ChangeToRed);
    }
}
