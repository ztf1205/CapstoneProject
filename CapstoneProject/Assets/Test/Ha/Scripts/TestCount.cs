using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCount : MonoBehaviour
{
    private int cubeCount;
    private int crystalCount;
    public UIManager UImanager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            if (cubeCount < UImanager.maxCube)
            {
                cubeCount++;
                other.gameObject.SetActive(false);
                UImanager.IncreaseCubeCount(cubeCount);
            }
        }
        else if (other.tag == "Crystal")
        {
            if (crystalCount < UImanager.maxCrystal)
            {
                crystalCount++;
                other.gameObject.SetActive(false);
                UImanager.IncreaseCrystalCount(crystalCount);
            }
        }
    }
}
