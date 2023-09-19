using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBallTest : SignalReceiver
{
    public Material red;
    public Material blue;

    protected override void ReceiverActivate()
    {
        GetComponent<MeshRenderer>().material = blue;
    }

    protected override void ReceiverDeactivate()
    {
        GetComponent<MeshRenderer>().material = red;
    }
}
