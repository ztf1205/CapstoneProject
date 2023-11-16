using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private static int count = 0;

    public static int Count() { return count; }
    public static void IncreaseCount() { count++; }
    public static void DecreaseCount() { count--; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IncreaseCount();
            gameObject.SetActive(false);
            EventManager.TriggerEvent("GainCube");
        }
    }
}
