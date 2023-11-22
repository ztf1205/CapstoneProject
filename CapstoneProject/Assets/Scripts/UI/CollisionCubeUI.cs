using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCubeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private float showDuration = 1f;

    private void Awake()
    {
        EventManager.Subscribe("OnSwitchDimensionFail", OnSwitchDimensionFail);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("OnSwitchDimensionFail", OnSwitchDimensionFail);
    }

    private void OnSwitchDimensionFail()
    {
        CancelInvoke("CubeDeactivate");
        CubeActivate();
    }

    private void CubeActivate()
    {
        cube.SetActive(true);
        Invoke("CubeDeactivate", showDuration);
    }

    private void CubeDeactivate()
    {
        cube.SetActive(false);
    }
}
