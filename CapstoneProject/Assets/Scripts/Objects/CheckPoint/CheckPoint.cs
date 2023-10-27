using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidbody = player.GetComponent<Rigidbody>();

        EventManager.Subscribe("Respawn", Respawn);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            EventManager.TriggerEvent("Respawn");
        }
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("Respawn", Respawn);
    }

    public void Respawn()
    {
        player.transform.position = transform.position;
        playerRigidbody.position = transform.position;
        playerRigidbody.velocity = Vector3.zero;
    }
}
