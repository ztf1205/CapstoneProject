using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using Invector.vCharacterController;

public class CinematicCamera : MonoBehaviour
{
    private DimensionManager dimManager;
    private Camera cam;
    private Camera threeDimensionCam;
    private Camera twoDimensionCam;

    // dolly zoom 관련 변수
    private Transform target; // player
    private float initHeightAtDist;
    private bool dzEnabled = false;

    private float lerpSpeed = 3f; // Lerp, Slerp 스피드
    private float zoomSpeed = 200f; // Dolly Zoom 스피드
    private float duration = 0.5f; // DOTween

    // 3D->2D 차원 전환 중 dolly zoom 직전 카메라 위치 관련 변수
    private float dy = 1f;
    private float dz = -8.5f;

    // field of view
    private float fovLowerLimit = 2f;
    private float fovUpperLimit = 60f;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
        cam = GetComponent<Camera>();
        threeDimensionCam = dimManager.threeDimensionCam.GetComponent<Camera>();
        twoDimensionCam = dimManager.twoDimensionCam.GetComponent<Camera>();

        cam.orthographicSize = dimManager.twoDimensionCam.GetComponent<TwoDimensionCamera>().DefaultOrthoSize;
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
            DollyZoomOut();
        }

        // 2D -> 3D
        else
        {
            DollyZoomIn();
        }
    }

    private void DollyZoomOut()
    {
        var currDistance = Vector3.Distance(transform.position, target.position);
        cam.fieldOfView = ComputeFieldOfView(initHeightAtDist, currDistance);

        // Lerp for position
        Vector3 targetPos = new Vector3(twoDimensionCam.transform.position.x, twoDimensionCam.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        transform.Translate(-(Vector3.forward) * zoomSpeed * Time.deltaTime);
        // Slerp for rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, twoDimensionCam.transform.rotation, lerpSpeed * Time.deltaTime);

        if (cam.fieldOfView <= fovLowerLimit)
        {
            dzEnabled = false;
            cam.orthographic = true;

            // Orthographic size 
            var td = twoDimensionCam.GetComponent<TwoDimensionCamera>();
            float duration = 1f;

            cam.DOOrthoSize(td.CurOrthoSize, duration)
                .OnComplete(() =>
                {
                    cam.enabled = false;
                    twoDimensionCam.enabled = true;
                });

            var input = target.gameObject.GetComponent<vThirdPersonInput>();
            input.MoveStopActivate(false);
        }
    }

    private void DollyZoomIn()
    {
        dzEnabled = false;

        Vector3 targetPos = new Vector3(target.position.x, target.position.y + dy, target.position.z + dz);
        transform.DOMove(targetPos, duration)
            .OnUpdate(() =>
            {
                var currDistance = Vector3.Distance(transform.position, target.position);
                cam.fieldOfView = ComputeFieldOfView(initHeightAtDist, currDistance);
                if (cam.fieldOfView >= fovUpperLimit)
                    AlignWith3DCamera();
            });
    }

    private void AlignWith3DCamera()
    {
        transform.DOMove(threeDimensionCam.transform.position, duration).SetEase(Ease.InOutSine);
        transform.DORotateQuaternion(threeDimensionCam.transform.rotation, duration).SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                cam.enabled = false;
                threeDimensionCam.enabled = true;

                var input = target.gameObject.GetComponent<vThirdPersonInput>();
                input.MoveStopActivate(false);
            });
    }

    public void CameraDirection()
    {
        var input = target.gameObject.GetComponent<vThirdPersonInput>();
        input.MoveStopActivate(true);

        // 3D -> 2D
        if (dimManager.Is2D)
        {
            // 3D 카메라 비활성화
            threeDimensionCam.enabled = false;
            cam.transform.position = threeDimensionCam.transform.position;
            cam.transform.rotation = threeDimensionCam.transform.rotation;
            cam.enabled = true;

            // Cinematic 카메라 이동 및 회전
            Vector3 targetPos = new Vector3(target.position.x, target.position.y + dy, target.position.z + dz);
            transform.DOMove(targetPos, duration);
            Vector3 targetRot = new Vector3(0f, 0f, transform.eulerAngles.z);
            transform.DORotate(targetRot, duration)
                .OnComplete(() =>
                {
                    // dolly zoom 시작 준비
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
            cam.transform.position = new Vector3(twoDimensionCam.transform.position.x, twoDimensionCam.transform.position.y, transform.position.z);
            cam.transform.rotation = twoDimensionCam.transform.rotation;
            cam.orthographic = false;
            cam.enabled = true;

            // dolly zoom 시작 준비
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
}
