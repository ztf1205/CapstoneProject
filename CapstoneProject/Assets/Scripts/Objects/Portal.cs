using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject portalExit;

    [SerializeField]
    private float portalWaitingTime = 1f;

    private GameObject player;
    private Rigidbody playerRigidbody;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("MovePlayer", portalWaitingTime);
    }

    private void OnCollisionExit(Collision collision)
    {
        CancelInvoke("MovePlayer");
    }

    private void MovePlayer()
    {
        player.transform.position = portalExit.transform.position;
        playerRigidbody.position = portalExit.transform.position;
        playerRigidbody.velocity = Vector3.zero;
    }
}
