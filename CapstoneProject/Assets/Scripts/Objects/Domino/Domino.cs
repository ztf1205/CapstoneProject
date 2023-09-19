using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject nextDomino;
    [SerializeField] private float forceMagnitude = 10.0f;

    private bool flag = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == nextDomino && !flag)
        {
            ApplyForceToNextDomino();
            flag = true;
        }
    }

    private void ApplyForceToNextDomino()
    {
        Vector3 forceDirection = nextDomino.transform.position - transform.position;
        forceDirection.Normalize();
        rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
    }
}
