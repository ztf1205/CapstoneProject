using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCollider : MonoBehaviour
{
    private GameObject checkPoint;

    [SerializeField] private GameObject checkPointFlag;

    private void Awake()
    {
        checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            checkPoint.transform.position = transform.position;

            gameObject.SetActive(false);
            checkPointFlag.SetActive(true);
        }
    }
}
