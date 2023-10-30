using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Camera threeDimensionCam;
    [SerializeField] private Camera twoDimensionCam;

    public bool Is2D { get; set; } = false;
    public bool CanSwitchDimension { get; set; } = true;

    //temp
    public Image image;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

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


    public void ChangeColor()
    {
        if (CanSwitchDimension)
            image.color = Color.green;
        else 
            image.color = Color.red;
    }
}
