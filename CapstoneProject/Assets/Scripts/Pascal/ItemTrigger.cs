using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] private Item acquiredItem;
    [SerializeField] private GameObject item;

    private static bool itemUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && acquiredItem.IsAcquired && !itemUsed)
        {
            EventManager.TriggerEvent("ItemTextOn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && acquiredItem.IsAcquired && !itemUsed)
        {
            EventManager.TriggerEvent("TextOff");
        }
    }

    private void Update()
    {
        if (acquiredItem.IsAcquired && !itemUsed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EventManager.TriggerEvent("TextOff");
                item.SetActive(true);
                itemUsed = true;
            }
        }
    }
}
