using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpPad : JumpPad
{
    [SerializeField] float superJumpHeight;

    public override void ChangeHeight()
    {
        jumpHeight = superJumpHeight;
        EventManager.TriggerEvent("SuperJumpPad");
    }
}