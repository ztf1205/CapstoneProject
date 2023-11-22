using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] int phase;

    void Start()
    {
        if (phase == 1)
            EventManager.Subscribe("OpenDoor1", OpenDoor);
        else
            EventManager.Subscribe("OpenDoor2", OpenDoor);
    }

    private void OpenDoor()
    {
        transform.DOMoveY(transform.position.y + 8f, 2f);
    }

    private void OnDestroy()
    {
        if (phase == 1)
            EventManager.Unsubscribe("OpenDoor1", OpenDoor);
        else
            EventManager.Unsubscribe("OpenDoor2", OpenDoor);
    }
}
