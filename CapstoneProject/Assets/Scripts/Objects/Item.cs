using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsAcquired { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !IsAcquired)
        {
            EventManager.TriggerEvent("GetItem");
            IsAcquired = true;
            gameObject.SetActive(false);
        }
    }
}
