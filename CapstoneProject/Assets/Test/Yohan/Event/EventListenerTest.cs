using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListenerTest : MonoBehaviour
{
    private void Awake()
    {
        EventManager.Subscribe("OnStart", PrintHelloWorld);
        EventManager.Subscribe("OnInputSpace", PlayJumpSound);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("OnStart", PrintHelloWorld);
        EventManager.Unsubscribe("OnInputSpace", PlayJumpSound);
    }

    private void PrintHelloWorld()
    {
        Debug.Log("Hello World");
    }

    private void PlayJumpSound()
    {
        Debug.Log("폴짝폴짝~!");
    }
}
