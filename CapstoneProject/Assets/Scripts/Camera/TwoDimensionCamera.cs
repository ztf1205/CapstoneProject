using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEditor;

public class TwoDimensionCamera : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Vector2 cameraOffset;
    private Vector3 camPos;
    private float dz = 20;

    private Camera cam;
    public float DefaultOrthoSize { get; private set; } = 5f;
    public float CurOrthoSize { get; private set; }

    public bool IsFollowingPlayer { get; set; } = true;
    private float pascalOrthoSize = 10f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camPos.z = player.position.z - dz;

        cam = GetComponent<Camera>();
        CurOrthoSize = DefaultOrthoSize;
        cam.orthographicSize = CurOrthoSize;
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
        camPos.x = player.position.x + cameraOffset.x;
        camPos.y = player.position.y + cameraOffset.y;
    }

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
