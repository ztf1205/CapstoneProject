using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<string, Action> eventTable = new Dictionary<string, Action>();

    public static void Subscribe(string eventName, Action listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] += listener;
        }
        else
        {
            eventTable[eventName] = listener;
        }
    }

    public static void Unsubscribe(string eventName, Action listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] -= listener;
            if (eventTable[eventName] == null)
            {
                eventTable.Remove(eventName);
            }
        }
    }

    public static void TriggerEvent(string eventName)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName]?.Invoke();
        }
    }
}


/* 사용 예제
 public class Player : MonoBehaviour
{
    private void Start()
    {
        // "PlayerDied" 이벤트에 구독
        EventManager.Subscribe("PlayerDied", PlayerDied);
    }

    private void PlayerDied()
    {
        Debug.Log("플레이어가 사망했습니다!");
    }

    private void EnemyCollision()
    {
        // 적과 충돌했을 때 플레이어 죽음
        EventManager.TriggerEvent("PlayerDied");
    }

    private void OnDestroy()
    {
        // 게임 오브젝트가 파괴될 때, 이벤트 구독 해제
        EventManager.Unsubscribe("PlayerDied", PlayerDied);
    }
}

 */