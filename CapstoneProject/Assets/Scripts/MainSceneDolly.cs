using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneDolly : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Camera targetCam;

    [SerializeField] private Transform target;

    private float initHeightAtDist;

    [SerializeField] private float duration = 1f;

    [SerializeField] private GameObject canvas;

    private void Start()
    {
        cam = GetComponent<Camera>();
        Invoke("PreDollyZoom", 1f);
    }

    private void PreDollyZoom()
    {
        Vector3 targetPos = new Vector3(target.position.x + 15f, target.position.y + 2f, target.position.z);
        transform.DOMove(targetPos, duration);
        Quaternion targetRot = targetCam.transform.rotation;
        transform.DORotateQuaternion(targetRot, duration)
            .OnComplete((TweenCallback)(() =>
            {
                float initDistance = Vector3.Distance(transform.position, target.position);
                initHeightAtDist = this.FrustumHeightAtDistance(initDistance);
                DollyZoom();
            }));
    }

    private void DollyZoom()
    {
        float targetX = transform.position.x + 500;
        transform.DOMoveX(targetX, duration).SetEase(Ease.InOutSine)
            .OnUpdate(() =>
            {
                var currDistance = Vector3.Distance(transform.position, target.position);
                cam.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
            })
            .OnComplete(() =>
            {
                AlignTo2DCamera();
            });
    }

    private void AlignTo2DCamera()
    {
        Vector3 targetPos = new Vector3(cam.transform.position.x, targetCam.transform.position.y, targetCam.transform.position.z);
        transform.DOMove(targetPos, duration)
            .OnComplete(() =>
            {
                cam.enabled = false;
                targetCam.enabled = true;
                canvas.SetActive(true);
            });
    }

    // Calculate the frustum height at a given distance from the camera
    private float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance 
    private float FOVForHeightAndDistance(float frustumHeight, float distance)
    {
        return 2.0f * Mathf.Atan(frustumHeight * 0.5f / distance) * Mathf.Rad2Deg;
    }
}
