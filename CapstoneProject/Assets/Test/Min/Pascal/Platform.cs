using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Platform : MonoBehaviour
{
    public enum State
    {
        ASCEND,
        DESCEND,
        RESET
    };
    public State MovingState {get;set;}

    private float totalPressure;
    public float TotalPressure => totalPressure;

    private float deltaY;
    public float DeltaY { get; set; }

    private float movingDuration = 5f;

    private float scaleX;

    private Vector3 initPosition;
    private Vector3 targetPosition;

    private PlatformManager manager;

    private void Start()
    {
        scaleX = transform.localScale.x / 12f;
        initPosition = transform.localPosition;

        manager = GameObject.FindObjectOfType<PlatformManager>().GetComponent<PlatformManager>();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Enter");
    //    if (collision.gameObject.name == "Player")
    //    {
    //        float mass = collision.rigidbody.mass;
    //        totalPressure += mass / scaleX;
    //    }
    //    else if (collision.gameObject.name == "Weight")
    //    {
    //        float mass = collision.rigidbody.mass;
    //        totalPressure += mass / scaleX;
    //    }

    //    manager.Move();
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("Exit");
    //    if (collision.gameObject.name == "Player")
    //    {
    //        float mass = collision.rigidbody.mass;
    //        totalPressure -= mass / scaleX;
    //    }
    //    else if (collision.gameObject.name == "Weight")
    //    {
    //        float mass = collision.rigidbody.mass;
    //        totalPressure -= mass / scaleX;
    //    }

    //    manager.Move();
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        float mass = other.attachedRigidbody.mass;
        totalPressure += mass / scaleX;

        manager.Move();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");

        float mass = other.attachedRigidbody.mass;
        totalPressure -= mass / scaleX;

        manager.Move();
    }

    public void Ascend(float deltaY)
    {
        //transform.DOKill();

        targetPosition = transform.localPosition + new Vector3(0f, deltaY, 0f);
        transform.DOMove(targetPosition, movingDuration);
    }

    public void Descend(float deltaY)
    {
        //transform.DOKill();

        targetPosition = transform.localPosition - new Vector3(0f, deltaY, 0f);
        transform.DOMove(targetPosition, movingDuration);
    }

    public void ResetPosition()
    {
        transform.DOKill();

        targetPosition = initPosition;
        transform.DOMove(targetPosition, movingDuration);
    }
}

