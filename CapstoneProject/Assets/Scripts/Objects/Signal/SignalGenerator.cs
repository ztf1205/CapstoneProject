using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SignalGenerator : MonoBehaviour
{
    private bool isActivate;
    public bool IsActivate => isActivate;

    protected virtual void GeneratorActivate()
    {
        isActivate = true;
    }

    protected virtual void GeneratorDeactivate()
    {
        isActivate = false;
    }
}