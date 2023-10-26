using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Water : MonoBehaviour
{
    public Vector3 InitPosition { get; set; }
    public float TargetPosY { get; set; }

    public Vector3 InitScale { get; set; }
    public float TargetScaleY { get; set; }

    private float movingDuration = 5f;

    // restoreRate 부피 변화량에 따라 시간 다르게 설정해야 함
    [SerializeField] private float restoringDuration = 5f;


    void Start()
    {
        InitPosition = transform.localPosition;
        InitScale = transform.localScale;
    }

    public void ChangeVolume()
    {
        transform.DOKill();

        Vector3 targetPosition = new Vector3(InitPosition.x, TargetPosY, InitPosition.z);
        transform.DOMove(targetPosition, movingDuration);

        Vector3 targetScale = new Vector3(InitScale.x, TargetScaleY, InitScale.z);
        transform.DOScale(targetScale, movingDuration);
    }

}
