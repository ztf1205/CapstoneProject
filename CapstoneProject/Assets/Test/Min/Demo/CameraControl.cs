using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TwoDimensionCamera td = camera.GetComponent<TwoDimensionCamera>();

            if (td.flag)
                td.ResetCamera();
            else
                td.MoveCamera();
        }
    }
}
