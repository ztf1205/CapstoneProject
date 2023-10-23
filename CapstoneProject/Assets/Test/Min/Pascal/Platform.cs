using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Platform : MonoBehaviour
{
    public Vector3 InitPosition { get; set; }
    public float TargetPosY { get; set; }

    public float Pressure { get; set; }

    public bool IsItemOn { get; set; } = false;
    public bool IsPlayerOn { get; set; } = false;

    private float movingDuration = 4f;

    private float scaleX;

    private PlatformManager manager;

    private void Start()
    {
        scaleX = transform.localScale.x / 3.026397f;
        InitPosition = transform.localPosition;

        manager = GameObject.FindObjectOfType<PlatformManager>().GetComponent<PlatformManager>();
    }

    private void SetPressure()
    {
        Pressure = 0;
        if (IsPlayerOn)
        {
            Pressure += (4 / scaleX);
            if (IsItemOn)
                Pressure += (1 / scaleX);
        }
        else
        {
            if (IsItemOn)
                Pressure += (2 / scaleX);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            IsPlayerOn = true;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
            IsItemOn = true;

        SetPressure();
        manager.Move();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "unitychan")
            IsPlayerOn = false;
        if (collision.gameObject.name == "Item")
            IsItemOn = false;

        SetPressure();
        manager.Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
            other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
            other.transform.SetParent(null);
    }

    public void ChangePosition()
    {
        transform.DOKill();

        Vector3 targetPosition = new Vector3(InitPosition.x, TargetPosY, InitPosition.z);
        transform.DOMove(targetPosition, movingDuration);
    }
}

