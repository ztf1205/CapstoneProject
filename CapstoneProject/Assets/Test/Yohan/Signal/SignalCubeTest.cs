using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalCubeTest : SignalGenerator
{
    public Material red;
    public Material blue;

    private void OnTriggerEnter(Collider other)
    {
        GeneratorActivate();
    }

    private void OnTriggerExit(Collider other)
    {
        GeneratorDeactivate();
    }

    protected override void GeneratorActivate()
    {
        base.GeneratorActivate();
        GetComponent<MeshRenderer>().material = blue;
    }

    protected override void GeneratorDeactivate()
    {
        base.GeneratorDeactivate();
        GetComponent<MeshRenderer>().material = red;
    }
}
