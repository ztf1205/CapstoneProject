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

    private float itemMass;
    private float playerMass;

    private float movingDuration = 4f;

    private float scaleX;

    private PlatformManager manager;

    private void Start()
    {
        if (this.gameObject.name == "PlatformA")
            scaleX = 1.0f;
        else if (this.gameObject.name == "PlatformB")
            scaleX = 2.0f;

        InitPosition = transform.localPosition;

        manager = GameObject.FindObjectOfType<PlatformManager>().GetComponent<PlatformManager>();
    }

    private void SetPressure()
    {
        Pressure = 0;
        if (IsPlayerOn)
        {
            Pressure += (playerMass / scaleX);
            if (IsItemOn)
                Pressure += (itemMass / scaleX);
        }
        else
        {
            if (IsItemOn)
                Pressure += (itemMass / scaleX);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayerOn = true;
            playerMass = collision.gameObject.GetComponent<Rigidbody>().mass;
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            IsItemOn = true;
            itemMass = collision.gameObject.GetComponent<Rigidbody>().mass;
        }
        SetPressure();
        manager.Move();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            IsPlayerOn = false;
        if (collision.gameObject.CompareTag("Item"))
            IsItemOn = false;

        SetPressure();
        manager.Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
            other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
            other.transform.SetParent(null);
    }

    public void ChangePosition()
    {
        transform.DOKill();

        Vector3 targetPosition = new Vector3(InitPosition.x, TargetPosY, InitPosition.z);
        transform.DOMove(targetPosition, movingDuration);
    }
}

