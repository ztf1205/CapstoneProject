using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YohanCameraTestSwap : MonoBehaviour
{
    public Transform pointA; // 지점 A의 Transform
    public Transform pointB; // 지점 B의 Transform
    public float moveDuration = 2.0f; // 이동 시간 (초)

    public List<GameObject> boxs;

    private Camera mainCamera;

    private bool isA = false;

    private void Start()
    {
        // DOTween 라이브러리 초기화
        DOTween.Init();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //위
            if(isA)
            {
                mainCamera.transform.DOKill();
                mainCamera.transform.DOMove(pointA.position, moveDuration).SetEase(Ease.Linear);
                mainCamera.transform.DORotateQuaternion(pointA.rotation, moveDuration).SetEase(Ease.Linear);
                isA = false;
                mainCamera.orthographic = false;

                foreach (var box in boxs)
                {
                    BoxCollider boxCollider = box.GetComponent<BoxCollider>();
                    boxCollider.size = new Vector3(1f, 1f, 1f);
                }
            }
            else//옆
            {
                mainCamera.transform.DOKill();
                mainCamera.transform.DOMove(pointB.position, moveDuration).SetEase(Ease.Linear);
                mainCamera.transform.DORotateQuaternion(pointB.rotation, moveDuration).SetEase(Ease.Linear);
                isA = true;
                mainCamera.orthographic = true;

                foreach (var box in boxs)
                {
                    BoxCollider boxcollider = box.GetComponent<BoxCollider>();
                    boxcollider.size = new Vector3(100f, 1f, 1f);
                }
            }
        }
    }
}
