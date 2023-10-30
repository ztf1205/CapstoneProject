using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableObject : MonoBehaviour
{
    private void Start()
    {
        EventManager.Subscribe("ResizeCollider", ResizeCollider);
        EventManager.Subscribe("ResetCollider", ResetCollider);
    }

    private void ResizeCollider()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 1f, 20f);
    }

    private void ResetCollider()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 1f, 1f);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("ResizeCollider", ResizeCollider);
        EventManager.Unsubscribe("ResetCollider", ResetCollider);
    }
}
