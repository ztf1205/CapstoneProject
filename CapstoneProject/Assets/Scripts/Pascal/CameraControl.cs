using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private TwoDimensionCamera td;
    [SerializeField] private bool isExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float playerX = other.transform.position.x;
            float triggerX = transform.position.x;
            float direction = playerX - triggerX;

            if (this.gameObject.name == "PascalTrigger")
            {
                if (!isExit)
                {
                    if (direction < 0)
                        td.PascalMoveCamera();
                    else
                        td.ResetCamera();
                }
               else
                {
                    if (direction < 0)
                        td.ResetCamera();
                    else
                        td.PascalMoveCamera();
                }
            }
        }
    }
}
