using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class Outlines : MonoBehaviour
{
    void Start()
    {
        EventManager.Subscribe("DrawOutline", DrawOutline);
    }

    private void DrawOutline()
    {
        
        foreach (Transform child in transform)
        {
            Outline childOutlineScript = child.GetComponent<Outline>();

            if (childOutlineScript != null)
            {
                childOutlineScript.eraseRenderer = false;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("DrawOutline", DrawOutline);
    }
}
