using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitchPlatform : MonoBehaviour
{
    [SerializeField]
    private float switchWaitingTime = 1f;

    private DimensionManager dimManager;

    private void Awake()
    {
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject .CompareTag("Player"))
        {
            Invoke("SwitchDimension", switchWaitingTime);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke("SwitchDimension");
        }
    }

    private void SwitchDimension()
    {
        EventManager.TriggerEvent("OnStartDollyZoom");
        dimManager.SwitchDimension();
    }
}
