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
    private Camera cinematicCam;
    private Camera threeDimensionCam;
    private Camera twoDimensionCam;

    // dolly zoom 관련 변수
    private Transform player; // player
    private float initHeightAtDist;
    private bool dzEnabled = false;

    private float lerpSpeed = 3f; // Lerp, Slerp 스피드
    private float zoomSpeed = 200f; // Dolly Zoom 스피드
    [SerializeField] private float duration = 0.5f; // DOTween

    // 3D->2D 차원 전환 중 dolly zoom 직전 카메라 위치 관련 변수
    private float dy = 1f;
    private float dz = -8.5f;

    // field of view
    private float fovLowerLimit = 2f;
    private float fovUpperLimit = 60f;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (dzEnabled)
            DollyZoom();
    }

    private void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dimManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
        cinematicCam = GetComponent<Camera>();
        threeDimensionCam = dimManager.threeDimensionCam.GetComponent<Camera>();
        twoDimensionCam = dimManager.twoDimensionCam.GetComponent<Camera>();

        cinematicCam.orthographicSize = dimManager.twoDimensionCam.GetComponent<TwoDimensionCamera>().DefaultOrthoSize;
        cinematicCam.fieldOfView = fovUpperLimit;
    }

    private void DollyZoom()
    {
        // 3D -> 2D
        if (dimManager.Is2D)
        {
            DollyZoom_3DTo2D();
        }

        // 2D -> 3D
        else
        {
            DollyZoom_2DTo3D();
        }
    }

    private void DollyZoom_3DTo2D()
    {
        var currDistance = Vector3.Distance(transform.position, player.position);
        cinematicCam.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);

        // Lerp for position
        Vector3 targetPos = new Vector3(twoDimensionCam.transform.position.x, twoDimensionCam.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        transform.Translate(-(Vector3.forward) * zoomSpeed * Time.deltaTime);
        // Slerp for rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, twoDimensionCam.transform.rotation, lerpSpeed * Time.deltaTime);

        if (cinematicCam.fieldOfView <= fovLowerLimit)
        {
            dzEnabled = false;
            cinematicCam.orthographic = true;

            cinematicCam.enabled = false;
            twoDimensionCam.enabled = true;

            EventManager.TriggerEvent("OnEndDollyZoom");
        }
    }

    private void DollyZoom_2DTo3D()
    {
        dzEnabled = false;

        Vector3 targetPos = new Vector3(player.position.x, player.position.y + dy, player.position.z + dz);
        transform.DOMove(targetPos, duration)
            .OnUpdate(() =>
            {
                var currDistance = Vector3.Distance(transform.position, player.position);
                cinematicCam.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
                if (cinematicCam.fieldOfView >= fovUpperLimit)
                    AlignTo3DCamera();
            });
    }

    private void AlignTo3DCamera()
    {
        transform.DOMove(threeDimensionCam.transform.position, duration).SetEase(Ease.InOutSine);
        transform.DORotateQuaternion(threeDimensionCam.transform.rotation, duration).SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                cinematicCam.enabled = false;
                threeDimensionCam.enabled = true;

                EventManager.TriggerEvent("OnEndDollyZoom");
            });
    }

    public void StartDollyZoom()
    {
        // 3D -> 2D
        if (dimManager.Is2D)
        {
            // 3D 카메라 비활성화
            threeDimensionCam.enabled = false;
            cinematicCam.transform.position = threeDimensionCam.transform.position;
            cinematicCam.transform.rotation = threeDimensionCam.transform.rotation;
            cinematicCam.enabled = true;

            // Cinematic 카메라 이동 및 회전
            Vector3 targetPos = new Vector3(player.position.x, player.position.y + dy, player.position.z + dz);
            transform.DOMove(targetPos, duration);
            Vector3 targetRot = new Vector3(0f, 0f, transform.eulerAngles.z);
            transform.DORotate(targetRot, duration)
                .OnComplete((TweenCallback)(() =>
                {
                    // dolly zoom 시작 준비
                    float initDistance = Vector3.Distance(transform.position, player.position);
                    initHeightAtDist = this.FrustumHeightAtDistance(initDistance);
                    dzEnabled = true;
                }));
        }

        // 2D -> 3D
        else
        {
            // 2D 카메라 비활성화
            twoDimensionCam.enabled = false;
            cinematicCam.transform.position = new Vector3(twoDimensionCam.transform.position.x, twoDimensionCam.transform.position.y, transform.position.z);
            cinematicCam.transform.rotation = twoDimensionCam.transform.rotation;
            cinematicCam.orthographic = false;
            cinematicCam.enabled = true;

            // dolly zoom 시작 준비
            float initDistance = Vector3.Distance(transform.position, player.position);
            initHeightAtDist = FrustumHeightAtDistance(initDistance);
            dzEnabled = true;
        }
    }

    // Calculate the frustum height at a given distance from the camera
    private float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(cinematicCam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance 
    private float FOVForHeightAndDistance(float frustumHeight, float distance)
    {
        return 2.0f * Mathf.Atan(frustumHeight * 0.5f / distance) * Mathf.Rad2Deg;
    }
}
