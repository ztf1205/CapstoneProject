using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScriptZoom : MonoBehaviour
{
    public Transform target;
    public Camera cameraCompnent;

    public float targetZPos = -100f;
    public float moveSpeed = 10f;


    private float initHeightAtDist;
    private bool dzEnabled;


    private Vector3 targetPosition; // 목표 위치

    private Vector3 startPosition;
    private float startTime;
    private float journeyLength;

    private bool isMoving = false;

    // Calculate the frustum height at a given distance from the camera.
    float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(cameraCompnent.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance.
    float FOVForHeightAndDistance(float frustumHeight, float distance)
    {
        return 2.0f * Mathf.Atan(frustumHeight * 0.5f / distance) * Mathf.Rad2Deg;
    }

    // Start the dolly zoom effect.
    void StartDZ()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(distance);
        dzEnabled = true;
    }

    // Turn dolly zoom off.
    void StopDZ()
    {
        dzEnabled = false;
    }

    void Start()
    {
        StartDZ();
    }

    void Update()
    {
        if (dzEnabled)
        {
            // Measure the new distance and readjust the FOV accordingly.
            var currDistance = Vector3.Distance(transform.position, target.position);
            cameraCompnent.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
        }


        // 스페이스바를 누르면 이동 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartMoving();
        }

        if (isMoving)
        {
            // 현재 시간 계산
            float distanceCovered = (Time.time - startTime) * moveSpeed;

            // 이동 비율 계산
            float journeyFraction = distanceCovered / journeyLength;

            // Lerp를 사용하여 부드럽게 이동
            transform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);

            // 이동이 완료되면 이동 중지
            if (journeyFraction >= 1.0f)
            {
                isMoving = false;
            }
        }
    }

    void StartMoving()
    {
        isMoving = true;
        startTime = Time.time;

        // 초기 위치와 이동 거리 설정
        startPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, transform.position.y, targetZPos);
        journeyLength = Vector3.Distance(startPosition, targetPosition);
    }
}