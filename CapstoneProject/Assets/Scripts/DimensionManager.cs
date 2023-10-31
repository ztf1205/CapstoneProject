using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    [SerializeField] private Camera threeDimensionCam;
    [SerializeField] private Camera twoDimensionCam;

    public bool Is2D { get; set; } = false;
    public bool CanSwitchDimension { get; set; } = true;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        SwitchCamera();
    }

    public void SwitchDimension()
    {
        Is2D = !Is2D;
        SwitchCamera();
        ResizeColliders();
    }

    private void SwitchCamera()
    {
        twoDimensionCam.enabled = Is2D;
        threeDimensionCam.enabled = !Is2D;
    }

    private void ResizeColliders()
    {
        string eventName = Is2D ? "ResizeCollider" : "ResetCollider";
        EventManager.TriggerEvent(eventName);
    }
}
