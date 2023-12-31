using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public GameObject threeDimensionCam;
    public GameObject twoDimensionCam;
    public GameObject twoDimensionPlayerCollisionCam;
    public GameObject cinematicCam;

    public bool isLevel4 = false;

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

    public void SwitchDimension(bool isDollyZoomMode)
    {
        Is2D = !Is2D;
        ResizeColliders();

        // 차원 전환 플랫폼
        if (isDollyZoomMode)
        {
            cinematicCam.GetComponent<CinematicCamera>().StartDollyZoom();
            SetOutlineEffect();
        }
        else
        {
            SwitchCamera();
            SetOutlineEffect();
        }

        TwoDimensionPlayerCollisionCamHandle();
    }

    private void TwoDimensionPlayerCollisionCamHandle()
    {
        twoDimensionPlayerCollisionCam.GetComponent<Camera>().enabled = Is2D;
        if (CanSwitchDimension)
        {
            twoDimensionPlayerCollisionCam.GetComponent<Camera>().enabled = false;
        }
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
