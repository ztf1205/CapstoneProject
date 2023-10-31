using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRotation : MonoBehaviour
{
    [SerializeField] private KeyCode rotationKey;
    [SerializeField] private string rotationAxis;
    private float rotationSpeed = 90.0f;

    private void Update()
    {
        if (Input.GetKeyDown(rotationKey))
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        if (rotationAxis == "X")
        {
            transform.Rotate(Vector3.right * rotationSpeed);
        }
        else if (rotationAxis == "Y")
        {
            transform.Rotate(Vector3.up * rotationSpeed);
        }
        else if (rotationAxis == "Z")
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
    }
}
