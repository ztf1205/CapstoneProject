using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEditor;
using Obi;

public class TwoDimensionCamera : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Vector2 cameraOffset;
    private Vector3 camPos;
    [SerializeField] private float dz = 4000f;

    [Header("Dead Zone")]
    [SerializeField] private Vector2 deadZone;
    [SerializeField] private Vector2 smoothingSpeed;

    private Camera cam;
    public float DefaultOrthoSize { get; private set; } = 5f;
    public float CurOrthoSize { get; private set; }

    public bool IsFollowingPlayer { get; set; } = true;

    private float pascalOrthoSize = 10f;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();

        camPos.x = player.position.x + cameraOffset.x;
        camPos.y = player.position.y + cameraOffset.y;
        camPos.z = player.position.z - dz;

        CurOrthoSize = DefaultOrthoSize;
        cam.orthographicSize = CurOrthoSize;

        transform.position = camPos;
    }

    private void FixedUpdate()
    {
        if (IsFollowingPlayer)
        {
            CalculateCameraPosition();
            transform.position = camPos;
        }
    }

    private void CalculateCameraPosition()
    {
        camPos.x = Mathf.Lerp(
            transform.position.x,
            player.position.x + cameraOffset.x,
            Time.deltaTime * smoothingSpeed.x);
        camPos.y = Mathf.Lerp(
            transform.position.y,
            player.position.y + cameraOffset.y,
            Time.deltaTime * smoothingSpeed.y);

        if (camPos.x - player.position.x > deadZone.x)
            camPos.x = player.position.x + deadZone.x;
        else if (camPos.x - player.position.x < -deadZone.x)
            camPos.x = player.position.x - deadZone.x;

        if (camPos.y - player.position.y > deadZone.y)
            camPos.y = player.position.y + deadZone.y;
        else if (camPos.y - player.position.y < -deadZone.y)
            camPos.y = player.position.y - deadZone.y;
    }

    // 연출
    public void PascalMoveCamera()
    {
        IsFollowingPlayer = false;

        float duration = 1f;
        Vector3 targetPos = new Vector3(player.position.x + 16f, transform.position.y - 2.5f, transform.position.z);
        CurOrthoSize = pascalOrthoSize;
;
        transform.DOMove(targetPos, duration);
        cam.DOOrthoSize(CurOrthoSize, duration);
    }

    public void ResetCamera()
    {
        IsFollowingPlayer = true;

        float duration = 1f;

        CurOrthoSize = DefaultOrthoSize;
        cam.DOOrthoSize(CurOrthoSize, duration);
    }
}
