using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : SignalGenerator
{
    [SerializeField] GameObject line;

    [SerializeField] Material red;
    [SerializeField] Material blue;

    private void OnCollisionEnter(Collision collision)
    {
        string tagName = collision.gameObject.tag;

        if (tagName == "Domino")
            GeneratorActivate();
    }

    private void OnCollisionExit(Collision collision)
    {
        string tagName = collision.gameObject.tag;

        if (tagName == "Domino")
            GeneratorDeactivate();
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
