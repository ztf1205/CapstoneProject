using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : SignalReceiver
{
    [SerializeField]
    private List<GameObject> targetGameObjects;

    [SerializeField]
    private bool setActive = true;

    protected override void ReceiverActivate()
    {
        foreach (var gameObject in targetGameObjects)
        {
            gameObject.SetActive(setActive);
        }
    }

    protected override void ReceiverDeactivate()
    {
        foreach (var gameObject in targetGameObjects)
        {
            gameObject.SetActive(setActive);
        }
    }
}
