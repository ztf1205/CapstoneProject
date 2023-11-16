using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] private Item acquiredItem;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject itemText;

    private static bool itemUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && acquiredItem.IsAcquired && !itemUsed)
        {
            itemText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && acquiredItem.IsAcquired && !itemUsed)
        {
            itemText.SetActive(false);
        }
    }

    private void Update()
    {
        if (acquiredItem && !itemUsed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                itemText.SetActive(false);
                item.SetActive(true);
                itemUsed = true;
            }
        }
    }
}
