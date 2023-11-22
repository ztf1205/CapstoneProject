using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }
}