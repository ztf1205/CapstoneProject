using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMill : SignalGenerator
{
    [SerializeField] GameObject line;

    [SerializeField] Material red;
    [SerializeField] Material blue;

    private float initZ;
    private float currentZ;

    private bool flag = false;

    private void Start()
    {
        initZ = transform.eulerAngles.z;
    }

    private void Update()
    {
        if (!flag)
            CheckZ();
    }

    private void CheckZ()
    {
        currentZ = transform.eulerAngles.z;
        if (Mathf.Abs(currentZ - initZ) > 330.0f)
        {
            flag = true;
            GeneratorActivate();
        }
    }

    protected override void GeneratorActivate()
    {
        base.GeneratorActivate();
        line.GetComponent<LineRenderer>().material = blue;
    }

    protected override void GeneratorDeactivate()
    {
        base.GeneratorDeactivate();
        line.GetComponent<LineRenderer>().material = red;
    }

}
