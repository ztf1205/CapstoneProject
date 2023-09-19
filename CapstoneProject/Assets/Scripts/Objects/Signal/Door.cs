using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class Door : SignalReceiver
{
    private float openSpeed = 1.0f;
    private float openHeight = 5.0f;

    private Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    private IEnumerator OpenDoorCoroutine()
    {
        Vector3 targetPos = initPos + Vector3.up * openHeight;
        while (transform.position != targetPos)
        {
            float step = openSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

            yield return null;
        }
    }

    private IEnumerator CloseDoorCoroutine()
    {
        Vector3 targetPos = initPos;
        {
            while (transform.position != targetPos)
            {
                float step = openSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

                yield return null;
            }
        }
    }

    protected override void ReceiverActivate()
    {
        StopAllCoroutines();
        StartCoroutine(OpenDoorCoroutine());
    }

    protected override void ReceiverDeactivate()
    {
        StopAllCoroutines();
        StartCoroutine(CloseDoorCoroutine());
    }
}
