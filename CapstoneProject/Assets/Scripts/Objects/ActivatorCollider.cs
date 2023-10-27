using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorCollider : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> targetGameObjects;

    [SerializeField]
    private bool setActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var gameObject in targetGameObjects)
            {
                gameObject.SetActive(setActive);
            }
        }
    }
}
