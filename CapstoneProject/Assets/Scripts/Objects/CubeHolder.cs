using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHolder : SignalGenerator
{
    [SerializeField] private GameObject cube;

    private bool isActivate = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Cube.Count() > 0 && isActivate == false)
        {
            GeneratorActivate();
            EventManager.TriggerEvent("CubP");
        }
    }

    protected override void GeneratorActivate()
    {
        base.GeneratorActivate();
        Cube.DecreaseCount();
        cube.SetActive(true);
        isActivate = true;
    }

    protected override void GeneratorDeactivate()
    {
        base.GeneratorDeactivate();
    }
}
