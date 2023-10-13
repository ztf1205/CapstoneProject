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

    private float unit = 3f;

    void Start()
    {
        pA = platformA.GetComponent<Platform>();
        pB = platformB.GetComponent<Platform>();

        waterA = waterCubeA.GetComponent<Water>();
        waterB = waterCubeB.GetComponent<Water>();
    }


    public void Move()
    {
        StateSetting();

        switch (pA.MovingState)
        {
            case Platform.State.ASCEND:
                pA.Ascend(pA.DeltaY);
                waterA.IncreaseVolume(pA.DeltaY);
                break;
            case Platform.State.DESCEND:
                pA.Descend(pA.DeltaY);
                waterA.DecreaseVolume(pA.DeltaY);
                break;
            case Platform.State.RESET:
                pA.ResetPosition();
                waterA.RestoreVolume();
                break;
            default:
                break;
        }

        switch (pB.MovingState)
        {
            case Platform.State.ASCEND:
                pB.Ascend(pB.DeltaY);
                waterB.IncreaseVolume(pB.DeltaY);
                break;
            case Platform.State.DESCEND:
                pB.Descend(pB.DeltaY);
                waterB.DecreaseVolume(pB.DeltaY);
                break;
            case Platform.State.RESET:
                pB.ResetPosition();
                waterB.RestoreVolume();
                break;
            default:
                break;
        }
    }

    private void StateSetting()
    {
        float pAtotalPressure = pA.TotalPressure;
        float pBtotalPressure = pB.TotalPressure;

        if (pAtotalPressure > pBtotalPressure)
        {
            float netPressure = (pAtotalPressure - pBtotalPressure);

            pA.DeltaY = netPressure * unit;
            pB.DeltaY = netPressure * unit / 2f;

            pA.MovingState = Platform.State.DESCEND;
            pB.MovingState = Platform.State.ASCEND;
        }
        else if (pBtotalPressure > pAtotalPressure)
        {
            float netPressure = (pBtotalPressure - pAtotalPressure);

            pA.DeltaY = netPressure * unit;
            pB.DeltaY = netPressure * unit / 2f;

            pA.MovingState = Platform.State.ASCEND;
            pB.MovingState = Platform.State.DESCEND;
        }
        else
        {
            pA.MovingState = Platform.State.RESET;
            pB.MovingState = Platform.State.RESET;
        }
    }
}
