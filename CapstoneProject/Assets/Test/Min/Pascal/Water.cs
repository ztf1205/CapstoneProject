using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Water : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 initScale;

    private float movingDuration = 5f;

    // restoreRate 부피 변화량에 따라 시간 다르게 설정해야 함
    [SerializeField] private float restoreDuration = 5f;

    private Vector3 targetScale;
    private Vector3 targetPosition;


    void Start()
    {
        initPosition = transform.localPosition;
        initScale = transform.localScale;
    }

    public void DecreaseVolume(float deltaY)
    {
        //transform.DOKill();

        targetScale = transform.localScale - new Vector3(0, deltaY, 0);
        transform.DOScale(targetScale, movingDuration);

        targetPosition = transform.localPosition - new Vector3(0, deltaY * 0.5f, 0);
        transform.DOMove(targetPosition, movingDuration);

    }

    public void IncreaseVolume(float deltaY)
    {
        //transform.DOKill();

        targetScale = transform.localScale + new Vector3(0, deltaY, 0);
        transform.DOScale(targetScale, movingDuration);

        targetPosition = transform.localPosition + new Vector3(0, deltaY * 0.5f, 0);
        transform.DOMove(targetPosition, movingDuration);
    }

    public void RestoreVolume()
    {
        transform.DOKill();

        targetScale = initScale;
        transform.DOScale(targetScale, movingDuration);

        targetPosition = initPosition;
        transform.DOMove(targetPosition, movingDuration);
    }
}
