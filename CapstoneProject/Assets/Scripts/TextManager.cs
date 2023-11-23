using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        EventManager.Subscribe("ItemTextOn", ItemTextOn);
        EventManager.Subscribe("SwitchTextOn", SwitchTextOn);
        EventManager.Subscribe("TextOff", TextOff);
    }

    private void ItemTextOn()
    {
        text.SetText("아이템을 사용하려면[E] 키를 누르세요.");
        text.enabled = true;
    }

    private void SwitchTextOn()
    {
        text.SetText("[E] 키를 눌러 상호작용하세요.");
        text.enabled = true;
    }

    private void TextOff()
    {
        text.enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("ItemTextOn", ItemTextOn);
        EventManager.Unsubscribe("SwitchTextOn", SwitchTextOn);
        EventManager.Unsubscribe("TextOff", TextOff);
    }
}
