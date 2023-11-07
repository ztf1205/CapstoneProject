using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    [SerializeField] private Camera threeDimensionCam;
    [SerializeField] private Camera twoDimensionCam;

    public bool Is2D { get; set; } = true;
    private bool canSwitchDimension;
    public bool CanSwitchDimension
    {
        get { return canSwitchDimension; }
        set
        {
            canSwitchDimension = value;
            string eventName = canSwitchDimension ? "ChangeToGreen" : "ChangeToRed";
            EventManager.TriggerEvent(eventName);
        }
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        SwitchDimension();
    }

    public void SwitchDimension()
    {
        Is2D = !Is2D;
        SwitchCamera();
        ResizeColliders();
        SetOutlineEffect();
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

    private void SetOutlineEffect()
    {
        if (Is2D)
        {
            OutlineEffect outlineEffect = threeDimensionCam.gameObject.GetComponent<OutlineEffect>();
            if (outlineEffect != null)
                DestroyImmediate(outlineEffect);
            twoDimensionCam.gameObject.AddComponent<OutlineEffect>();
        }
        else
        {
            OutlineEffect outlineEffect = twoDimensionCam.gameObject.GetComponent<OutlineEffect>();
            if (outlineEffect != null)
                DestroyImmediate(outlineEffect);
            threeDimensionCam.gameObject.AddComponent<OutlineEffect>();
        }
    }
}
