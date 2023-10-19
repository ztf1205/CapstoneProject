using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VcamManager : MonoBehaviour
{
    [SerializeField] private GameObject TpsCamera;
    [SerializeField] private GameObject SubCamera;
    [SerializeField] private float waitTime = 10.0f; 

    private bool isTps = true;

    // Update is called once per frame
    void Update()
    {
        changeCamera();
    }

    public void changeCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isTps)
            {
                TpsCamera.gameObject.SetActive(false);
                SubCamera.gameObject.SetActive(true);
                isTps = false;
            }
            else
            {
                TpsCamera.gameObject.SetActive(true);
                SubCamera.gameObject.SetActive(false);
                isTps = true;
            }
        }
    }
}
