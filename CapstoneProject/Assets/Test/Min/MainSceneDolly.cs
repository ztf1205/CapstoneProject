using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneDolly : MonoBehaviour
{
    [SerializeField] private Camera targetCam;

    [SerializeField] private Transform target;

    private float initHeightAtDist;
    private bool dzEnabled = false;

    private float lerpSpeed = 3f; // Lerp, Slerp 스피드
    private float zoomSpeed = 200f; // Dolly Zoom 스피드
    [SerializeField] private float duration = 0.5f; // DOTween

    private void Start()
    {
        Vector3 targetPos = new Vector3(target.position.x + 5f, target.position.y + 2f, target.position.z);

        Vector3 targetRot = new Vector3(0f, 0f, -90f);
    }
}
