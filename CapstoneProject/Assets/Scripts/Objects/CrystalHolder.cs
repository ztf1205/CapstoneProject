using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHolder : SignalGenerator
{
    public bool CanHoldCrystal { get; set; } = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && CanHoldCrystal)
        {
            GeneratorActivate();
            EventManager.TriggerEvent("CrysP");
        }
    }

    protected override void GeneratorActivate()
    {
        base.GeneratorActivate();
        EventManager.TriggerEvent("ActivateCrystal");
    }

    protected override void GeneratorDeactivate()
    {
        base.GeneratorDeactivate();
    }
}
