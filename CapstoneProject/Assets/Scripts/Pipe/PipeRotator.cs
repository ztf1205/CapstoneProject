using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRotator : MonoBehaviour
{
    public GameObject pipe;
    public string rotationAxis;

    private bool canRotate = false;

    private void Update()
    {
        if (canRotate && Input.GetKeyDown(KeyCode.E))
        {
            RotatePipe();
            EventManager.TriggerEvent("RotatePipe");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canRotate = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canRotate = false;
        }
    }


    private void RotatePipe()
    {
        if (rotationAxis == "X")
        {
            pipe.transform.Rotate(Vector3.right * 90f);
        }
        else if (rotationAxis == "Y")
        {
            pipe.transform.Rotate(Vector3.up * 90f);
        }
        else if (rotationAxis == "Z")
        {
            pipe.transform.Rotate(Vector3.forward * 90f);
        }
    }
}
