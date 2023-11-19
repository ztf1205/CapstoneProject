using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    protected float jumpHeight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            vThirdPersonController cc = other.gameObject.GetComponent<vThirdPersonController>();

            // 점프대 디폴트 높이값: 7.5
            jumpHeight = cc.originalJumpHeight * 1.5f;
            ChangeHeight();
            cc.SuperJump(jumpHeight);
            EventManager.TriggerEvent("JumpPad");
        }
    }

    public virtual void ChangeHeight()
    {

    }
}
