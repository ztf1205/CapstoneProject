using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SignalReceiver : MonoBehaviour
{
    [SerializeField]
    private List<SignalGenerator> signals;

    private bool isActivate;
    protected bool IsActivate => isActivate;

    protected virtual void Update()
    {
        SignalCheck();
    }

    private void SignalCheck()
    {
        //신호 체크
        bool activateCheck = true;

        foreach (var signal in signals)
        {
            if (signal.IsActivate == false)
            {
                activateCheck = false;
                break;
            }
        }

        //상태 바뀌었으면 활성화 및 비활성화 처리
        if(isActivate != activateCheck)
        {
            isActivate = activateCheck;
            if (isActivate)
            {
                ReceiverActivate();
            }
            else
            {
                ReceiverDeactivate();
            }
        }
    }
    
    protected abstract void ReceiverActivate();
    protected abstract void ReceiverDeactivate();
}
