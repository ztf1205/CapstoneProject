using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableObject : MonoBehaviour
{
    private Collider objectCollider;

    private BoxCollider boxCollider = null;
    private CapsuleCollider capsuleCollider = null;

    private Vector3 originalSize;
    private float originalHeight;

    [SerializeField] private bool isCollision2DEnabled = true;

    private DimensionManager dimManager;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        objectCollider = GetComponent<Collider>();
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();

        if (objectCollider is BoxCollider)
        {
            boxCollider = (BoxCollider)objectCollider;
            originalSize = boxCollider.size;
        }
        else if (objectCollider is CapsuleCollider)
        {
            capsuleCollider = (CapsuleCollider)objectCollider;
            originalHeight = capsuleCollider.height;
        }
    }

    private void Start()
    {
        EventManager.Subscribe("ResizeCollider", ResizeCollider);
        EventManager.Subscribe("ResetCollider", ResetCollider);
    }

    private void Update()
    {
        if (dimManager.Is2D)
            objectCollider.enabled = isCollision2DEnabled;
        else
            objectCollider.enabled = true;
    }

    private void ResizeCollider()
    {
        if (boxCollider != null)
            boxCollider.size = new Vector3(originalSize.x, originalSize.y, 999999f);

        if (capsuleCollider != null)
            capsuleCollider.height = 999999f;
    }

    private void ResetCollider()
    {
        if (boxCollider != null)
            boxCollider.size = originalSize;

        if (capsuleCollider != null)
            capsuleCollider.height = originalHeight;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("ResizeCollider", ResizeCollider);
        EventManager.Unsubscribe("ResetCollider", ResetCollider);
    }
}
