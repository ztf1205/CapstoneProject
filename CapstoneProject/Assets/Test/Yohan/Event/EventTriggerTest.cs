using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerTest : MonoBehaviour
{
    private void Start()
    {
        EventManager.TriggerEvent("OnStart");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.TriggerEvent("OnInputSpace");
        }
    }
}
