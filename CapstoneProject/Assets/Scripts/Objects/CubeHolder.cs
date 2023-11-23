using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHolder : SignalGenerator
{
    [SerializeField] private GameObject cube;

    private bool isEmpty = true;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Cube.Count() > 0 && isEmpty == true)
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
        isEmpty = false;
    }

    protected override void GeneratorDeactivate()
    {
        base.GeneratorDeactivate();
    }
}
