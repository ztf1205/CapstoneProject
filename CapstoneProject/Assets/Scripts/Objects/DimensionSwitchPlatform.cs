using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitchPlatform : MonoBehaviour
{
    private DimensionManager dimManager;

    private void Awake()
    {
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        dimManager.SwitchDimension();
    }
}
