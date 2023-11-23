using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private TwoDimensionCamera td;
    [SerializeField] private bool isExit;

    private bool pascalFlag = false;
    private bool resetFlag = false;

    private void Start()
    {
        EventManager.Subscribe("Move2DCameraWhenRespawn", Move2DCameraWhenRespawn);
    }

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
                    //if (direction < 0 && !pascalFlag)
                    //{
                    //    pascalFlag = true;
                    //    resetFlag = false;
                    //    td.PascalMoveCamera();
                    //}
                    //else if (direction > 0 && !resetFlag)
                    //{
                    //    resetFlag = true;
                    //    pascalFlag = false;
                    //    td.ResetCamera();
                    //}
                    if (!pascalFlag)
                    {
                        pascalFlag = true;
                        td.PascalMoveCamera();
                    }
                }
               else
                {
                    //if (direction < 0 && !resetFlag)
                    //{
                    //    resetFlag = true;
                    //    pascalFlag = false;
                    //    td.ResetCamera();
                    //}

                    //else if (direction > 0 && !pascalFlag)
                    //{
                    //    pascalFlag = true;
                    //    resetFlag = false;
                    //    td.PascalMoveCamera();
                    //}
                    td.ResetCamera();
                }
            }
        }
    }

    private void Move2DCameraWhenRespawn()
    {
        if (pascalFlag)
        {
            var tdCamera = td.GetComponent<TwoDimensionCamera>();
            tdCamera.IsFollowingPlayer = true;


            var cam = td.GetComponent<Camera>();
            cam.orthographicSize = tdCamera.DefaultOrthoSize;

            pascalFlag = false;
            resetFlag = false;

        }
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("Move2DCameraWhenRespawn", Move2DCameraWhenRespawn);
    }
}
