using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject platformA;
    [SerializeField] GameObject platformB;
    private Platform pA;
    private Platform pB;

    [SerializeField] GameObject waterCubeA;
    [SerializeField] GameObject waterCubeB;
    private Water waterA;
    private Water waterB;

    private float unit = 0.7f;

    void Start()
    {
        pA = platformA.GetComponent<Platform>();
        pB = platformB.GetComponent<Platform>();

        waterA = waterCubeA.GetComponent<Water>();
        waterB = waterCubeB.GetComponent<Water>();
    }

    private void SetTarget()
    {
        float netPressure = (pA.Pressure - pB.Pressure);

        pA.TargetPosY = pA.InitPosition.y - netPressure * unit;
        pB.TargetPosY = pB.InitPosition.y + netPressure * unit / 2f;

        waterA.TargetPosY = waterA.InitPosition.y - netPressure * unit / 2f;
        waterB.TargetPosY = waterB.InitPosition.y + netPressure * unit / 2f / 2f;
        waterA.TargetScaleY = waterA.InitScale.y - netPressure * unit;
        waterB.TargetScaleY = waterB.InitScale.y + netPressure * unit / 2f;
    }

    public void Move()
    {
        SetTarget();

        pA.ChangePosition();
        waterA.ChangeVolume();
        pB.ChangePosition();
        waterB.ChangeVolume();
    }
}
