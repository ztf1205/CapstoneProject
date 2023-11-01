using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableObject : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Vector3 originalSize;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        originalSize = boxCollider.size;
    }

    private void Start()
    {
        EventManager.Subscribe("ResizeCollider", ResizeCollider);
        EventManager.Subscribe("ResetCollider", ResetCollider);
    }

    private void ResizeCollider()
    {
        boxCollider.size = new Vector3(originalSize.x, originalSize.y, 999999f);
    }

    private void ResetCollider()
    {
        boxCollider.size = originalSize;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("ResizeCollider", ResizeCollider);
        EventManager.Unsubscribe("ResetCollider", ResetCollider);
    }
}
