using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RotateY : MonoBehaviour
{
    [Label("회전 속도 (도/초)"),SerializeField]
    private float rotationSpeed = 30f;

    private void Update()
    {
        // Y축 주위로 회전
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
