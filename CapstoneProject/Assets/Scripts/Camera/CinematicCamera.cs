using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CinematicCamera : MonoBehaviour
{
    private DimensionManager dimManager;
    private Camera cam;
    private Camera threeDimensionCam;
    private Camera twoDimensionCam;

    [SerializeField] private Transform target; // player
    private float initHeightAtDist;
    private bool dzEnabled = false;

    [SerializeField] private float zoomSpeed = 150f; // Dolly Zoom 스피드
    [SerializeField] private float duration = 0.3f; // DOTween용 duration

    private float fovLowerLimit = 3f;
    private float fovUpperLimit = 60f;

    void Start()
    {
        Init();
        EventManager.Subscribe("AlignWith2DCamera", AlignWith2DCamera);
        EventManager.Subscribe("AlignWith3DCamera", AlignWith3DCamera);
    }

    private void Init()
    {
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
        cam = GetComponent<Camera>();
        threeDimensionCam = dimManager.threeDimensionCam.GetComponent<Camera>();
        twoDimensionCam = dimManager.twoDimensionCam.GetComponent<Camera>();

        cam.fieldOfView = fovUpperLimit;
    }


    private void Update()
    {
        if (dzEnabled)
            DollyZoom();
    }
    
    private void DollyZoom()
    {
        // 3D -> 2D
        if (dimManager.Is2D)
        {
            var currDistance = Vector3.Distance(transform.position, target.position);
            cam.fieldOfView = ComputeFieldOfView(initHeightAtDist, currDistance);
            transform.Translate(-Vector3.forward * Time.deltaTime * zoomSpeed);
            if (cam.fieldOfView <= fovLowerLimit)
            {
                dzEnabled = false;
                cam.orthographic = true;
                EventManager.TriggerEvent("AlignWith2DCamera");
            }
        }
        // 2D -> 3D
        else
        {
            cam.orthographic = false;
            var currDistance = Vector3.Distance(transform.position, target.position);
            cam.fieldOfView = ComputeFieldOfView(initHeightAtDist, currDistance);
            transform.Translate(Vector3.forward * Time.deltaTime * zoomSpeed);
            if (cam.fieldOfView >= fovUpperLimit)
            {
                dzEnabled = false;
                EventManager.TriggerEvent("AlignWith3DCamera");
            }
        }
    }


    public void CameraDirection()
    {
        // 3D -> 2D
        if (dimManager.Is2D)
        {
            // 3D 카메라 비활성화
            threeDimensionCam.enabled = false;
            cam.transform.position = threeDimensionCam.transform.position;
            cam.transform.rotation = threeDimensionCam.transform.rotation;
            cam.enabled = true;

            // Cinematic 카메라 이동 및 회전 (dolly zoom 준비)
            Vector3 targetPos = new Vector3(target.position.x, target.position.y + 1f, target.position.z - 8.5f);
            transform.DOMove(targetPos, duration);
            Vector3 targetRot = new Vector3(0f, 0f, transform.eulerAngles.z);
            transform.DORotate(targetRot, duration)
                .OnComplete(() =>
                {
                    // dolly zoom
                    float initDistance = Vector3.Distance(transform.position, target.position);
                    initHeightAtDist = ComputeFrustumHeight(initDistance);
                    dzEnabled = true;
                });
        }
        // 2D -> 3D
        else
        {
            // 2D 카메라 비활성화
            twoDimensionCam.enabled = false;
            cam.transform.position = twoDimensionCam.transform.position;
            cam.fieldOfView = 13; // temp
            cam.enabled = true;

            // Cinematic 카메라 이동 및 회전 (dolly zoom 준비)
            Vector3 targetPos = new Vector3(target.position.x, target.position.y + 1f, target.position.z - 150f);
            transform.DOMove(targetPos, duration);

            float initDistance = Vector3.Distance(transform.position, target.position);
            initHeightAtDist = ComputeFrustumHeight(initDistance);
            dzEnabled = true;
        }

        
    }

    // Calculate the frustum height at a given distance from the camera
    private float ComputeFrustumHeight(float distance)
    {
        return 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance 
    private float ComputeFieldOfView(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    private void AlignWith2DCamera()
    {
        // Ortho도 필요할 예정
        transform.DOMove(twoDimensionCam.transform.position, duration).SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                cam.enabled = false;
                twoDimensionCam.enabled = true;
            });
    }

    private void AlignWith3DCamera()
    {
        transform.DOMove(threeDimensionCam.transform.position, duration).SetEase(Ease.InOutSine);
        transform.DORotateQuaternion(threeDimensionCam.transform.rotation, 0.3f).SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                cam.enabled = false;
                threeDimensionCam.enabled = true;
            });
    }
}
