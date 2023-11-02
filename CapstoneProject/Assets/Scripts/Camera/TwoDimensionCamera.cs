using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class TwoDimensionCamera : MonoBehaviour
{
    private Transform target;
    [SerializeField] Vector3 offset;

    public bool flag = false;

    private void Start()
    {
        //Init();
    }

    private void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(target.position.x, target.position.y, -80f);
    }

    private void LateUpdate()
    {
        //if (!flag)
        //{
        //    Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z) + offset;
        //    transform.position = targetPosition;
        //}
    }

    public void MoveCamera()
    {
        flag = true;
        float duration = 3f;
        float targetX = -13f;
        float targetSize = 10f;
        transform.DOKill();
        Camera.main.DOKill();
        transform.DOMoveX(targetX, duration)
        .OnUpdate(() =>
        {
            Camera.main.DOOrthoSize(targetSize, duration);
        });
    }

    public void ResetCamera()
    {
        flag = false;
        float targetSize = 5f;
        float duration = 3f;
        Camera.main.DOKill();
        Camera.main.DOOrthoSize(targetSize, duration);
    }
}
