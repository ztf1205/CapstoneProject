using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableObject : MonoBehaviour
{
    private Collider collider;

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
        collider = GetComponent<Collider>();
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();

        if (collider is BoxCollider)
        {
            boxCollider = (BoxCollider)collider;
            originalSize = boxCollider.size;
        }
        else if (collider is CapsuleCollider)
        {
            capsuleCollider = (CapsuleCollider)collider;
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
            collider.enabled = isCollision2DEnabled;
        else
            collider.enabled = true;
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
