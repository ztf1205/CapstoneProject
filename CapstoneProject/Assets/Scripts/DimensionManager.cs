using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public GameObject threeDimensionCam;
    public GameObject twoDimensionCam;
    public GameObject cinematicCam;

    public bool Is2D { get; set; } = false;
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
        SwitchCamera();
        ResizeColliders();
        SetOutlineEffect();
    }

    public void SwitchDimension()
    {
        Is2D = !Is2D;
        ResizeColliders();
        SetOutlineEffect();
        cinematicCam.GetComponent<CinematicCamera>().CameraDirection();
     
    }

    private void SwitchCamera()
    {
        twoDimensionCam.GetComponent<Camera>().enabled = Is2D;
        threeDimensionCam.GetComponent<Camera>().enabled = !Is2D;
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
            OutlineEffect outlineEffect = threeDimensionCam.GetComponent<OutlineEffect>();
            if (outlineEffect != null)
                DestroyImmediate(outlineEffect);
            twoDimensionCam.AddComponent<OutlineEffect>();
        }
        else
        {
            OutlineEffect outlineEffect = twoDimensionCam.GetComponent<OutlineEffect>();
            if (outlineEffect != null)
                DestroyImmediate(outlineEffect);
            threeDimensionCam.AddComponent<OutlineEffect>();
        }
    }
}
