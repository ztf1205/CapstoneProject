using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        EventManager.Subscribe("ItemTextOn", ItemTextOn);
        EventManager.Subscribe("SwitchTextOn", SwitchTextOn);
        EventManager.Subscribe("SkillTextOn", SkillTextOn);
        EventManager.Subscribe("TextOff", TextOff);
    }

    private void ItemTextOn()
    {
        text.SetText("아이템을 사용하려면 [E]키를 누르세요.");
        canvas.enabled = true;
    }

    private void SwitchTextOn()
    {
        text.SetText("[E]키를 눌러 상호작용하세요.");
        canvas.enabled = true;
    }

    private void SkillTextOn()
    {
        text.SetText("차원 스위치 스킬을 획득하였습니다!" + "\n" + "마우스 왼쪽 버튼을 클릭해보세요.");
        canvas.enabled = true;
        Invoke("TextOff", 3f);
    }

    private void TextOff()
    {
        canvas.enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("ItemTextOn", ItemTextOn);
        EventManager.Unsubscribe("SwitchTextOn", SwitchTextOn);
        EventManager.Unsubscribe("SkillTextOn", SkillTextOn);
        EventManager.Unsubscribe("TextOff", TextOff);
    }
}
